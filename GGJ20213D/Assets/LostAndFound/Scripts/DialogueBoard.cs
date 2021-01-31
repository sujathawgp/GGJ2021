using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoard : MonoBehaviour
{
    public Transform target;
    public TMP_Text dialogText;    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target, Vector3.up);
        transform.LookAt(transform.position + target.rotation * Vector3.forward,
            target.rotation * Vector3.up);
    }
}
