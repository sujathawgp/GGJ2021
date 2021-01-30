using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCamera : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;
    Vector3 playerPos = Vector3.zero;
    Vector3 camPos = Vector3.zero;
    Vector3 offset = Vector3.zero;

    void Start()
    {
        camPos = cameraTransform.position;
        playerPos = playerTransform.position;
        offset = camPos - playerPos;
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        cameraTransform.position = playerTransform.position + offset;
    }
}
