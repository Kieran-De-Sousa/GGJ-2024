using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public int timeMinutes = 5;
    public int timeSeconds = 0;
    public int timer;
    
    private float _timerCounter = 1;

    private GameObject _gameController;

    private void Awake()
    {
        timer = (timeMinutes * 60) + timeSeconds;

        _gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    private void Update()
    {
        _timerCounter -= 1 * Time.deltaTime;
        if (_timerCounter <= 0)
        {
            _timerCounter = 1;

            timer -= 1;
            
            timeMinutes = timer / 60;
            timeSeconds = timer % 60;
            
            SetString(timeMinutes, timeSeconds);
            
            _gameController.GetComponent<BreakableObjectSpawningScript>().SpawnObject(timer);
        }
    }

    public void SetString(int timeMinute, int timeSecond)
    {
        string minuteString = timeMinute.ToString();
        if (minuteString.Length == 1)
        {
            minuteString = "0" + minuteString;
        }

        string secondString = timeSecond.ToString();
        if (secondString.Length == 1)
        {
            secondString = "0" + secondString;
        }

        GetComponent<TMP_Text>().text = minuteString + " : " + secondString;
    }
}
