using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PuppyStates
{
    Normal,
    Confused,
    Happy,
};

public class PlayerData : MonoBehaviour
{
    public float health = 100.0f;
    public float smellRange = 10.0f;
    public PuppyStates puppyState = PuppyStates.Normal;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
