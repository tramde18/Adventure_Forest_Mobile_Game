using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertPlayerForEnemies : MonoBehaviour
{
    public AudioClip wolfHowlFX;
    AudioSource soundManager;
    bool isPlayed = false;
    void Start()
    {
        soundManager = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isPlayed && other.tag == "Player")
        {
            isPlayed = true;
            soundManager.PlayOneShot(wolfHowlFX);
            CharacterController2D.instance.ShowBubbleChat("It's scary here...");
        }
    }
}
