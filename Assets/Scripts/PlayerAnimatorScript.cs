using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    public GameObject targetAnimator;
    private Animator playerAnim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnim = targetAnimator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleRun(bool run)
    {
        playerAnim.SetBool("IsRunning",run);
    }

    public void ToggleAttack(bool attack) 
    {
        
        playerAnim.SetBool("Attacking", attack);

    }

    public void GrappleAnim()
    {
        playerAnim.SetTrigger("Grapple");
    }

    public void Grab()
    {
        playerAnim.SetTrigger("Grab");
    }

    public void Repel()
    {
        playerAnim.SetTrigger("Repel");
    }
}
