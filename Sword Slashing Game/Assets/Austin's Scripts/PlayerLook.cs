using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private Transform playerBody;

    private PlayerControls controller;
    private Vector2 controllerRotation;

    private float xAxisClamp;

    private void Awake()
    {
        LockCursor();
        controller = new PlayerControls();
        controller.Gameplay.CameraRotation.performed += context => controllerRotation = context.ReadValue<Vector2>();
        controller.Gameplay.CameraRotation.canceled += context => controllerRotation = Vector2.zero;
        //controller.Gameplay.CameraRotation.performed += context => Debug.Log("moved joystick");
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        float controllerX = controllerRotation.x * mouseSensitivity * Time.deltaTime;
        float controllerY = controllerRotation.y * mouseSensitivity * Time.deltaTime;

        //Debug.Log("controllerX: " + controllerX);

        xAxisClamp += mouseY + controllerY;

        if(xAxisClamp > -18.0f)
        {
            xAxisClamp = -18.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(18.0f);

        } else if(xAxisClamp < -25.0f)
        {
            xAxisClamp = -25.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(25.0f);
        }

        transform.Rotate(Vector3.left * (mouseY + controllerY));
        playerBody.Rotate(Vector3.up * (mouseX + controllerX));
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

    //enabling and disabling the controller inputs
    void OnEnable()
    {
        controller.Gameplay.Enable();
    }

    void OnDisable()
    {
        controller.Gameplay.Disable();
    }
}
