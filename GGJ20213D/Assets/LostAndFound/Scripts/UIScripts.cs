using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{
    public PlayerData player;

    public Image HealthBar;
    private float MaxHealth = 100.0f;


    void Start()
    {
    }

    void Update()
    {
        HealthBar.fillAmount = (player.health + 10) / MaxHealth;
    }

}
