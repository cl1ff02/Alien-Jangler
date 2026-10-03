using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform playerTransform;
    public bool isMagnetic = true; // Set True for Swarmer, False for Grunt
    public float moveSpeed = 3.5f;

    //Attack Settings
    public float attackRange = 8f; 
    public float attackCooldown = 1.2f;  
    public GameObject projectilePrefab;   // Drag projectile prefab here
    public Transform firePoint;           // Spawn point for the projectile
    public float projectileSpeed = 10f;  

    private Rigidbody rb;
    private float attackTimer = 0f;
    private float knockbackTimer = 0f;
    private bool isKnockedBack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Keep upright
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        // Auto-locate player by PlayerMovement, Player script, or Tag
        if (playerTransform == null)
        {
            PlayerMovement movementScript = Object.FindFirstObjectByType<PlayerMovement>();
            if (movementScript != null)
            {
                playerTransform = movementScript.transform;
            }
            else if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }
    
    void Update()
    {
        // Attack cooldown
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        // Handle knockback / magnetic duration
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
        if (playerTransform == null || isKnockedBack) return;

        // Look towards the player
        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.LookAt(targetPosition);

        // Move towards the player using Rigidbody
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (rb != null)
        {
            rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        }

        // Attack distance check
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange && attackTimer <= 0)
        {
            PerformAttack();
            attackTimer = attackCooldown;
        }
    }

    void PerformAttack()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning(gameObject.name + " has no projectilePrefab assigned!");
            return;
        }

        // Determine spawn point
        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position + transform.forward * 0.8f;
        
        // Spawn projectile facing the player
        GameObject proj = Instantiate(projectilePrefab, spawnPos, transform.rotation);
        
        // Give projectile velocity moving forward
        Rigidbody projRb = proj.GetComponent<Rigidbody>();
        if (projRb != null)
        {
            projRb.velocity = transform.forward * projectileSpeed;
        }
    }

    public void ApplyMagnetForce(Vector3 force, float duration)
    {
        if (!isMagnetic) return; // Grunts ignore magnetic force

        force.y = 0f; // Constrain force to horizontal plane

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            rb.AddForce(force, ForceMode.Impulse);
        }

        isKnockedBack = true;
        knockbackTimer = duration;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detects magnetic zone collision
        if (other.CompareTag("MagnetZone") || other.GetComponent<MagnetBehavior>() != null)
        {
            Vector3 pushDirection = (transform.position - other.transform.position).normalized;
            ApplyMagnetForce(pushDirection * 10f, 0.5f);
        }
    }
}
