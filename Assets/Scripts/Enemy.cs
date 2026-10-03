using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth;
    [SerializeField] GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            if (enemyHealth - 1 != 0)
            {
                enemyHealth -= 1;
            }
            else
            {
                enemyHealth = 0;
                Destroy(gameObject);
            }
            Debug.Log(enemyHealth);
        }
    }
}
