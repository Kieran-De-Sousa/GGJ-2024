using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private int timeMinutes = 2;
    [SerializeField] private int timeSeconds = 0;
    [SerializeField] private int timer;
    [SerializeField] private int eventTimer = 5;
    [SerializeField] private Vector2 eventTimerRange = new Vector2(1, 15);
    
    
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
            _timerCounter += 1;

            timer -= 1;
            if (timer <= 0)
            {
                if (FindObjectOfType<LaughBarScript>().fillAmount >= 90)
                {
                    // Win
                    GameData.winner = ScoreManager.Instance.GetHighestScore();
                    SceneManager.LoadScene("WinScreen");
                }
                else
                {
                    // Lose
                    SceneManager.LoadScene("LoseScreen");
                }
            }
            
            timeMinutes = timer / 60;
            timeSeconds = timer % 60;
            
            SetString(timeMinutes, timeSeconds);
            
            _gameController?.GetComponent<BreakableObjectSpawningScript>()?.SpawnObject(timer);

            eventTimer -= 1;
            if (eventTimer <= 0)
            {
                eventTimer = GetNewTimer();
                
                _gameController.GetComponent<FunnyEventsControllerScript>().SummonRandomEvent();
            }
        }
    }

    private int GetNewTimer()
    {
        int newTimer = (int)(Random.Range(eventTimerRange.x, eventTimerRange.y));

        return newTimer;
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
