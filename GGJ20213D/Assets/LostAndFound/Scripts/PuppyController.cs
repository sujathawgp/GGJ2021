using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PuppyController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveCtrl;
    [SerializeField] private InputActionReference jumpCtrl;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 4.0f;

    private CharacterController controller;
    private Vector3 playerVelocity = Vector3.zero;
    private bool groundedPlayer;

    private Transform cameraMainTransform;

    public PlayerData playerData;

    private AudioManager audioManager;

    private float speedBoost = 1f;

    private void OnEnable()
    {
        moveCtrl.action.Enable();
        jumpCtrl.action.Enable();
    }

    private void OnDisable()
    {
        moveCtrl.action.Disable();
        jumpCtrl.action.Disable();
    }


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playerData = gameObject.GetComponent<PlayerData>();
        cameraMainTransform = Camera.main.transform;
        audioManager = FindObjectOfType<AudioManager>();
    }


    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = moveCtrl.action.ReadValue<Vector2>();
        if (movement != Vector2.zero)
        {
            audioManager.Play("walk", transform.position);
        }
        else
        {
            audioManager.Stop("walk");
        }

        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        move.y = 0;

        controller.Move(move * Time.deltaTime * playerSpeed * speedBoost);

        if(speedBoost > 1f)
        {
            speedBoost -= 0.001f;
        }
        else
        {
            speedBoost = 1f;
        }

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        if (jumpCtrl.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            audioManager.Play("jump", transform.position);
        } 

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("OnCollisionEnter");

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (hit.gameObject.tag == "SpeedMushroom")
        {
            SpeedMushroomScript mushroom = hit.gameObject.GetComponent<SpeedMushroomScript>();
            if (mushroom.CanEat())
            {
                audioManager.Play("interaction", hit.gameObject.transform.position);
                mushroom.Eat();
                speedBoost = 5;
            }
        }

    }

}
