using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    private UIScripts uiScripts;

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
        uiScripts = GameObject.Find("Canvas").GetComponent<UIScripts>();
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
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (hit.gameObject.tag == "SpeedMushroom" || hit.gameObject.tag == "PoisonPlant")
        {
            EatablesScript eatable = hit.gameObject.GetComponent<EatablesScript>();
            if (eatable && eatable.CanEat())
            {
                audioManager.Play("interaction", hit.gameObject.transform.position);
                eatable.Eat();

                 if (hit.gameObject.tag == "SpeedMushroom")
                {
                    speedBoost = Mathf.Min(speedBoost + 5, 10);
                    audioManager.Play("wee", transform.position);
                    uiScripts.BeginDisplayDialogue("Look at me shooting like a rocket!!");
                }
                else if (hit.gameObject.tag == "PoisonPlant")
                {
                    playerData.health = Mathf.Max(playerData.health - 20, 0);
                    audioManager.Play("cough", transform.position);
                    uiScripts.BeginDisplayDialogue("That does not taste right ***");
                }
            }
           
        }

        if (hit.gameObject.tag == "Bone")
        {
            EatablesScript eatable = hit.gameObject.GetComponent<EatablesScript>();
            if (eatable && eatable.CanEat())
            {
                audioManager.Play("bark", hit.gameObject.transform.position);
                eatable.Eat();
                playerData.bonesCount++;
                uiScripts.BeginDisplayDialogue("Got Bone!!");
            }
        }

        if (hit.gameObject.tag == "Sniffable" || hit.gameObject.tag == "Mushroom")
        {
            audioManager.Play("sniff", hit.gameObject.transform.position);
        }
        if (hit.gameObject.name == "Exit")
        {
            Debug.Log(hit.gameObject.name);
            //Game over
            if(playerData.alive)
            {
                //audioManager.Play("win", Vector3.zero);
                SceneManager.LoadScene("Win");
            }
        }

    }

}
