using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputAction wasd;
    public InputAction arrows;
    public float speed = 1.0f;

    private CharacterController controller;

    void OnEnable()
    {
        wasd.Enable();
        arrows.Enable();
    }

    void OnDisable()
    {
        wasd.Disable();
        arrows.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent(typeof(CharacterController)) as CharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = wasd.ReadValue<Vector2>();
        controller.Move(input * Time.deltaTime * speed);

    }
}
