using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int bonesCount = 0;

    public bool alive = true;

    //public int gameTotalTime = 10 * 60; // mins * seconds. Move to GameManager
    public int gameTotalTime = 1 * 60; // mins * seconds. Move to GameManager

    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(BeingAlive());
    }

    void Update()
    {
        if(!alive)
        {
            //audioManager.Play("lose", Vector3.zero);
            SceneManager.LoadScene("Lose");
        }
    }

    IEnumerator BeingAlive()
    {
        yield return new WaitForSeconds(gameTotalTime);
    }

}
