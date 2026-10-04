using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 3;
    [SerializeField] private GameObject player;
    public int damageAmount = 1;

    void Start()
    {
        if (player == null)
        {
            Player playerScript = Object.FindFirstObjectByType<Player>();
            if (playerScript != null)
            {
                player = playerScript.gameObject;
            }
            else if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemyHealth -= damageAmount;
            Debug.Log(gameObject.name + " Health: " + enemyHealth);

            if (enemyHealth <= 0)
            {
                Die();
            }
        }        
    }
    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        Debug.Log(gameObject.name + " Health: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            Die();
        }     
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has been defeated.");
        Destroy(gameObject);
    }
}
