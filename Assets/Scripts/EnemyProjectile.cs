using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 4f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<Player>() != null || other.GetComponent<PlayerMovement>() != null)
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null && playerScript.playerHealth > 0)
            {
                playerScript.playerHealth -= damage;
                
                if (playerScript.playerHealth <= 0)
                {
                    Destroy(other.gameObject);
                }
            }

            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy") && !other.CompareTag("MagnetZone"))
        {
            Destroy(gameObject);
        }
    }
}
