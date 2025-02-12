using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public int m_scorePoints;
    public AudioClip m_soundFX;
    AudioSource m_SoundManager;
    Text m_scoreUI;
    Timer m_timer;

    private void Start()
    {
        //m_scoreUI = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        m_timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        m_SoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //int TotalScore = int.Parse(m_scoreUI.text.ToString()) + m_scorePoints;
            //m_scoreUI.text = TotalScore.ToString();
            float TotalRemainingTime = m_timer.m_timeRemaining + m_scorePoints;
            m_timer.m_timeRemaining = TotalRemainingTime;
            m_SoundManager.PlayOneShot(m_soundFX);
            CharacterController2D.instance.ShowBubbleChat("Yeeeeey!!!");
            Destroy(gameObject);
        }
    }
}
