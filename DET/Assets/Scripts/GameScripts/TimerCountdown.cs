using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TimerCountdown : MonoBehaviour
{
    Text text;
    public int minutesLeft;
    float timeLeft;
    public bool takingAway = false;
    GameObject player;
    PlayerHealth playerHealth;
    


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = GetComponent<Text>();
        timeLeft = minutesLeft * 10;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {   
        
        if(takingAway == false && timeLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
        if(timeLeft == 0)
        {
            playerHealth.currentHealth = 0;
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        timeLeft -= 1;
        TimeSpan time = TimeSpan.FromSeconds(timeLeft);
        text.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
        takingAway = false;
    }
}
