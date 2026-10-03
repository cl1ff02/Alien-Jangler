using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Basic Settings")]
    public Transform playerTransform;
    public bool isMagnetic = true; // True for Swarmer, False for Grunt

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

    private MagnetBehavior targetMagnet;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock rotation so the enemy does not fall over
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        targetMagnet = Object.FindFirstObjectByType<MagnetBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetMagnet != null)
    {
        float distance = Vector3.Distance(transform.position, targetMagnet.transform.position);
        
        if (distance < 3.0f && !isKnockedBack)
        {
            Vector3 pushDirection = (transform.position - targetMagnet.transform.position).normalized;
            ApplyMagnetForce(pushDirection * 12f, 0.5f);
        }
    }
        
    // Countdown for attack cooldown
    if (attackTimer > 0)
    {
        attackTimer -= Time.deltaTime;
    }

    // Handle knockback timer
    if (isKnockedBack)
    {
        knockbackTimer -= Time.deltaTime;
        if (knockbackTimer <= 0)
        {
            isKnockedBack = false;
            rb.linearVelocity = Vector3.zero; // Stop moving after knockback
        }
    }
    }

    void FixedUpdate()
    {
        if (playerTransform == null) return;
        if (isKnockedBack) return; // Stop walking if knocked back

        // 1. Face the player
        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.LookAt(targetPosition);

        // 2. Move towards the player
        Vector3 direction = (targetPosition - transform.position);
        rb.MovePosition(transform.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);

        // 3. Attack check
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange && attackTimer <= 0)
        {
            AttackPlayer();
            attackTimer = attackCooldown; // Reset timer
        }
    }

    void AttackPlayer()
    {
        Debug.Log(gameObject.name + " attacked the player!");
        // Put player damage call here later
    }

    // Call this when player uses Q magnet skill
    public void ApplyMagnetForce(Vector3 force, float duration)
    {
        if (!isMagnetic) return; // Grunts ignore magnet!

        force.y = 0f; // Keep on flat floor
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(force, ForceMode.Impulse);

        isKnockedBack = true;
        knockbackTimer = duration;
    }
}
