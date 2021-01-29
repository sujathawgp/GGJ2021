using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    public InputAction wasd;
    public InputAction jump;
    public float speed = 1.0f;
    
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.8f;
    private CharacterController controller;
    //private Rigidbody rigidbodyComponent;
    private Vector3 playerVelocity;
    private Vector3 directionVector;

    void Awake()
    {
        // If jump is performed, call Jump()
        jump.performed += ctx => Jump();
        wasd.performed += ctx => Move(ctx.ReadValue<Vector2>());
        wasd.canceled += ctx => directionVector = Vector3.zero;
    }
    void OnEnable()
    {
        wasd.Enable();
        jump.Enable();
    }

    void OnDisable()
    {
        wasd.Disable();
        jump.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent(typeof(CharacterController)) as CharacterController;
        //rigidbodyComponent = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
    }

    // Update is called once per frame
    void Update()
    {
         if(controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        // take care of jump and y axis value
        playerVelocity.y += gravityValue * Time.deltaTime; // update gravity

        Vector3 FinalInput = new Vector3(directionVector.x, playerVelocity.y, directionVector.y);
        controller.Move(FinalInput * Time.deltaTime * speed);
        Vector3 forwardInput = new Vector3(FinalInput.x, 0, FinalInput.z);

        if(forwardInput != Vector3.zero)
        {
            // Mark x,0,z as forward vector
            gameObject.transform.forward = forwardInput;
        }

    }

    void Jump()
    {
        // based from https://docs.unity3d.com/ScriptReference/CharacterController.Move.html?_ga=2.267172174.1716293476.1611665569-728552274.1609928915
        if(controller.isGrounded)
        {
            // jump with respect to gravity
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    void Move(Vector2 direction)
    {
        directionVector = direction;
    }
}
