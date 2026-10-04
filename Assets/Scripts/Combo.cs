using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Combo : MonoBehaviour
{
    private int comboCounter = 0;
    private float comboCooldown = 5f;
    private bool comboOnCD = false;
    private int attackDamage = 1;
    InputAction attackAction;
    GameObject enemy;

    //Aniamtor
    [SerializeField] GameObject playerModel;
    private Animator playerAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        GameObject.Find("Enemy").GetComponent<Enemy>().damageAmount = attackDamage;
        enemy = GameObject.Find("Enemy");

        playerAnim = playerModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider collider)
    {
        Debug.Log("Something is in range");
        if(collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy is getting hit!");
            if(comboOnCD == false)
            {
                if (attackAction.IsPressed())
                {
                    if (comboCounter + 1 <= 3)
                    {
                        playerAnim.SetBool("Attacking", true);

                        switch (comboCounter)
                        {
                            case 0:
                                playerAnim.SetTrigger("Attack_1");
                                break;
                            case 1:
                                playerAnim.SetTrigger("Attack_2");
                                break;
                            case 2:
                                playerAnim.SetTrigger("Attack_3");
                                break;

                             

                        }

                        collider.gameObject.GetComponent<Enemy>().TakeDamage(attackDamage);
                        Debug.Log($"Enemy took: {attackDamage} damage!");
                        comboCounter += 1;
                        Debug.Log(comboCounter);
                    }
                    else
                    {
                        comboCounter = 0;
                        comboOnCD = true;
                    }
                }
            }
            else
            {
                comboCooldown -= Time.deltaTime;

                if(comboCooldown <= 0)
                {
                    comboOnCD = false;
                    comboCooldown = 2f;
                }
                Debug.Log(comboCooldown);
            }
        }
    }
}
