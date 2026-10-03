using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Basic Settings")]
    public Transform playerTransform;
    public bool isMagnetic = true; // Set True for Swarmer, False for Grunt

    [Header("Movement Settings")]
    public float moveSpeed = 3.5f;

    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public int attackDamage = 1;
    public float attackCooldown = 1.2f;

    private Rigidbody rb;
    private float attackTimer = 0f;
    private float knockbackTimer = 0f;
    private bool isKnockedBack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock X and Z rotation to prevent the enemy from tipping over
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        // Automatically locate the player in the scene
        if (playerTransform == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // Attack cooldown timer
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        // Handle knockback / magnetic movement duration
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockedBack = false;
                if (rb != null)
                {

                    rb.linearVelocity = Vector3.zero;

                    rb.velocity = Vector3.zero;

                }
            }
        }
    }

    void FixedUpdate()
    {
        if (playerTransform == null) return;
        if (isKnockedBack) return; // Freeze movement during knockback

        // 1. Face towards the player
        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.LookAt(targetPosition);

        // 2. Move towards the player
        Vector3 direction = (targetPosition - transform.position);
        if (rb != null)
        {
            rb.MovePosition(transform.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
        }

        // 3. Attack check
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange && attackTimer <= 0)
        {
            AttackPlayer();
            attackTimer = attackCooldown;
        }
    }

    void AttackPlayer()
    {
        Debug.Log(gameObject.name + " attacked the player!");

        // Add player health deduction call here if applicable
    }

    /// <summary>
    /// Applies force to the enemy when affected by magnet abilities or external physics impulses.
    /// </summary>
    public void ApplyMagnetForce(Vector3 force, float duration)
    {
        if (!isMagnetic) return; // Grunts ignore magnetic forces

        force.y = 0f; // Keep movement on the horizontal plane

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;

            rb.velocity = Vector3.zero;

            rb.AddForce(force, ForceMode.Impulse);
        }

        isKnockedBack = true;
        knockbackTimer = duration;
    }

    // Automatically detect trigger overlap with magnet zones without editing teammate scripts
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagnetZone") || other.GetComponent<MagnetBehavior>() != null)
        {
            Vector3 pushDirection = (transform.position - other.transform.position).normalized;
            ApplyMagnetForce(pushDirection * 10f, 0.5f);
        }
    }
}
