using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image HealthBar;
    public float CurrentHealth;
    private float MaxHealth = 100.0f;

    PlayerInput playerInputScript;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GetComponent<Image>();
        playerInputScript = FindObjectOfType<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
        //CurrentHealth = playerInputScript.health;
        //HealthBar.fillAmount = CurrentHealth / MaxHealth;
    }
}
