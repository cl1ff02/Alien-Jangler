using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimCameraController : MonoBehaviour
{
    [SerializeField] private Transform yawTarget;
    [SerializeField] private Transform pitchTarget;
    [SerializeField] private InputActionReference lookInput;
    [SerializeField] private InputActionReference switchShoulderInput;
    [SerializeField] private float mouseSensitivity = 0.05f;
    [SerializeField] private float pitchMin = -40f;
    [SerializeField] private float pitchMax = 80f;
    [SerializeField] private CinemachineThirdPersonFollow aimCam;
    [SerializeField] private float shoulderSwitchSpeed = 5f;

    private float yaw;
    private float pitch;
    private float targetCameraSide;

    void Awake()
    {
        aimCam = GetComponent<CinemachineThirdPersonFollow>();
        targetCameraSide = aimCam.CameraSide;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 angles = yawTarget.rotation.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
        lookInput.asset.Enable();
    }
    private void OnEnable()
    {
        switchShoulderInput.action.Enable();
        switchShoulderInput.action.performed += OnSwitchShoulder;        
    }

    private void OnDisable()
    {
        switchShoulderInput.action.Disable();
        switchShoulderInput.action.performed -= OnSwitchShoulder;    
    }

    private void OnSwitchShoulder(InputAction.CallbackContext context)
    {
        targetCameraSide = aimCam.CameraSide < 0.5f ? 1f : 0f; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 look = lookInput.action.ReadValue<Vector2>();
        look *= mouseSensitivity;
        yaw += look.x * mouseSensitivity;
        pitch -= look.y * mouseSensitivity;
        yawTarget.rotation = Quaternion.Euler(0f, yaw, 0f);
        pitchTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        aimCam.CameraSide = Mathf.Lerp(aimCam.CameraSide, targetCameraSide, Time.deltaTime * shoulderSwitchSpeed);
    }
    public void SetYawPitchFromCameraForward(Transform cameraTransform)
    {
        Vector3 flatForward = cameraTransform.forward;
        flatForward.y = 0;
        if (flatForward.sqrMagnitude < 0.001f)
        {
            return;
        }
        yaw = Quaternion.LookRotation(flatForward).eulerAngles.y;
        yawTarget.rotation = Quaternion.Euler(0f, yaw, 0f);
        pitchTarget.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
