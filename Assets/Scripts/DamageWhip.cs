using UnityEngine;

public class DamageWhip : MonoBehaviour
{
    private int attackDamage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Enemy>().TakeDamage(attackDamage);
        Debug.Log($"Enemy took: {attackDamage} damage!");
    }
}
