using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerHealth;
    [SerializeField] GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == enemy)
        {
            if (playerHealth - 1 != 0)
            {
                playerHealth -= 1;
            }
            else
            {
                playerHealth = 0;
                Destroy(gameObject);
            }
            Debug.Log(playerHealth);
        }
    }
}
