using System;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrab : MonoBehaviour
{
    InputAction grabAction;
    InputAction lookAction;
    Vector2 pos;
    private float xRotation;
    public float sensitivity;
    public Transform playerbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //lookAction = InputSystem.actions.FindAction("Look");
        //pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        grabAction = InputSystem.actions.FindAction("Interact");
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 lookValue = lookAction.ReadValue<Vector2>();
        //float mouseX = lookValue.x * sensitivity * Time.deltaTime;
        //float mouseY = lookValue.y * sensitivity * Time.deltaTime;
        //xRotation -= mouseY;
        //xRotation = Math.Clamp(xRotation, -50f, 50f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //playerbody.Rotate(Vector3.up * mouseX);
        //Debug.Log(pos);
    }
}
