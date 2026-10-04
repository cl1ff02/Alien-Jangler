using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFighting : MonoBehaviour
{
    InputAction repelAction;
    public float repelStrength;
    Rigidbody rb;

    //Aniamtor
    
    private PlayerAnimatorScript playerAnimScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        repelAction = InputSystem.actions.FindAction("Repel");
        GameObject.Find("Enemy").GetComponent<Enemy>().damageAmount = 1;

        playerAnimScript = GetComponent<PlayerAnimatorScript>();

    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = player.position * distance;
        if (repelAction.IsPressed())
        {
            ApplyForce();
            playerAnimScript.Repel();
            //playerAnimScript.Repel();
        }
    }
    void ApplyForce()
    {
        rb.AddForce(0, 0, repelStrength, ForceMode.Impulse);
        Debug.Log($"Force has been applied to: {rb}");
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy is inside the range");
            rb = other.gameObject.GetComponent<Rigidbody>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            rb = null;
        }
    }
}
