using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Health Settings
    public int enemyHealth = 3;

    //Target Settings
    [SerializeField] private GameObject player;

    void Start()
    {
        // Automatically find the player object if not assigned
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
