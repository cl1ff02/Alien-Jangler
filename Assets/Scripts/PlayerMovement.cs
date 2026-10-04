using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = -9.8f;
    private CharacterController controller;
    private Vector3 moveInput;
    public Vector3 velocity;
    public bool isAiming;
    public int collectable;
    public Camera cam;
    private bool isGrounded;
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
        move = Vector3.ClampMagnitude(move, 1f);
        transform.rotation = Camera.main.transform.rotation; //object used to constantly rotate after pressing movement, credit to https://www.youtube.com/watch?v=VPfhVWrjktI
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            if(velocity.y < -2f)
                velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        Vector3 finalMove = move * speed + Vector3.up * velocity.y;
        controller.Move(Camera.main.transform.rotation * finalMove * Time.deltaTime);
        if (controller.velocity.x == 0 && controller.velocity.y == 0)
        {
            playerAnimScript.ToggleRun(false);
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectable")
        {
            collectable += 1;
            Destroy(other.gameObject);
        }
    }
}
