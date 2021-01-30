using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{
    private Image HealthBar;
    
    public float CurrentHealth;
    
    private float MaxHealth = 100.0f;

    PlayerData player;

    void Start()
    {
        HealthBar = GameObject.Find("HealthBarUI").GetComponent<Image>();
        player = FindObjectOfType<PlayerData>();
    }

    void Update()
    {
        CurrentHealth = player.health;
        if(HealthBar)
        {
            HealthBar.fillAmount = CurrentHealth / MaxHealth;
        }
        else
        {
            Debug.Log("health bar not found");
        }
    }

}
