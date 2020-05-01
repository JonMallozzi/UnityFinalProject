using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    private CharacterController cController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    private bool isJumping;

    private PlayerControls controller;
    private Vector2 characterRotation;

    private void Awake()
    {
        cController = GetComponent<CharacterController>();
        controller = new PlayerControls();
        controller.Gameplay.Movement.performed += context => characterRotation = context.ReadValue<Vector2>();
        controller.Gameplay.Movement.canceled += context => characterRotation = Vector2.zero;
    }

    private void Update()
    {
        PlayerMovement();
        JumpInput();
    }

    private void PlayerMovement()
    {
        float hInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vInput = Input.GetAxis(verticalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vInput;
        Vector3 rightMovement = transform.right * hInput;

        cController.SimpleMove(forwardMovement + rightMovement);

        //rotate character when moving 
        Vector2 rotation = new Vector2(0, characterRotation.x) * 100f * Time.deltaTime;
        transform.Rotate(rotation, Space.World);


    }

    private void JumpInput() 
    {
        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        } 
    }

    private IEnumerator JumpEvent()
    {
        cController.slopeLimit = 90.0f;
        float airTime = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(airTime);
            cController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            airTime += Time.deltaTime;
            yield return null;
        } while (!cController.isGrounded && cController.collisionFlags != CollisionFlags.Above);

        cController.slopeLimit = 45.0f;
        isJumping = false;
        
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
