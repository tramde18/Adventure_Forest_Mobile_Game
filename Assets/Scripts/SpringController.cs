using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    bool m_onTop;
    GameObject m_bouncer;
    Animator m_anim;
    AudioSource m_SoundManager;
    public Vector2 m_velocity;
    public AudioClip m_soundFX;

    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_SoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (m_onTop)
        {
            m_anim.SetBool("isStepped", true);
            m_bouncer = other.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_onTop = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_onTop = false;
        m_anim.SetBool("isStepped", false);
    }

    public void Jump()
    {
        if (m_bouncer.tag == "Player")
        {
            m_bouncer.GetComponent<Rigidbody2D>().velocity = m_velocity;
            m_bouncer.GetComponent<Animator>().Play("Jump");
            m_SoundManager.PlayOneShot(m_soundFX);
            CharacterController2D.instance.CreateDust();
            CharacterController2D.instance.ShowBubbleChat("Woohooo!!!");
        }
    }
}
