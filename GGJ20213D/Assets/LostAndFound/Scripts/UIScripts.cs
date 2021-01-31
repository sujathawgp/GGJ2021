using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScripts : MonoBehaviour
{
    public PlayerData player;

    public Image HealthBar;
    private float MaxHealth = 100.0f;
    public TextMeshProUGUI bonesCountText;
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI timerText;

    void Start()
    {
        HealthBar = GameObject.Find("HealthBarUI").GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<PlayerData>();
        bonesCountText = GameObject.Find("BonesCountText").GetComponent<TextMeshProUGUI>();
        bonesCountText.text = "Bones: 0";
        dialogue = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();

        StartCoroutine(DisplayTimer());
    }

    void Update()
    {
        HealthBar.fillAmount = (player.health + 10) / MaxHealth;
        bonesCountText.text = "Bones: " + player.bonesCount;
    }

    public void BeginDisplayDialogue(string text)
    {
        StartCoroutine(DisplayDialogue(text, 3));
    }

    IEnumerator DisplayDialogue(string text, int delay)
    {
        dialogue.text = text;
 
        yield return new WaitForSeconds(delay); 

        dialogue.text = "";
    }

    IEnumerator DisplayTimer()
    {
        int timer = player.gameTotalTime;
        while(timer > 0)
        {
            yield return new WaitForSeconds(1); 
            timer--;
            timerText.text = "Time: " + timer;
        }
        player.alive = false;
    }
 
}
