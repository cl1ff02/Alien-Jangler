using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private CharacterController controller;
    private Vector3 moveInput;
    public Vector3 velocity;
    public bool isAiming;

    //Aniamtor

    private PlayerAnimatorScript playerAnimScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        playerAnimScript = GetComponent<PlayerAnimatorScript>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        playerAnimScript.ToggleRun(true);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * speed * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(move); //object used to constantly rotate after pressing movement, credit to https://www.youtube.com/watch?v=VPfhVWrjktI

        if (controller.velocity.x == 0 && controller.velocity.y == 0)
        {
            playerAnimScript.ToggleRun(false);
        }
        
    }
}
