using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float m_timeRemaining = 10;
    public bool m_timerIsRunning = false;
    Text m_TimerUI;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_TimerUI = GetComponent<Text>();
    }

    void Update()
    {
        if (m_timerIsRunning)
        {
            if (m_timeRemaining > 0)
            {
                m_timeRemaining -= Time.deltaTime;
                DisplayTime(m_timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                Gameplay.instance.GameOver();
                m_timeRemaining = 0;
                m_timerIsRunning = false;
                DisplayTime(0f);
            }
        }
    }

    public void StartTimer()
    {
        Time.timeScale = 1f;
        m_timerIsRunning = true;
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        m_TimerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
