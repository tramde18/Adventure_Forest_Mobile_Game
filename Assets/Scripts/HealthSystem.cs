using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem instance;
    public int m_totalLives = 3;
    public GameObject m_livesPrefab;
    public AudioClip oneUpFX, m_hurtFX;
    private GameObject m_lives;
    private AudioSource m_SoundManager;

    

    private void Awake()
    {
        instance = this;
        m_SoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();
    }

    public void AddLives()
    {
        m_lives = Instantiate(m_livesPrefab);
        m_lives.transform.parent = this.transform;
        m_lives.transform.localScale = new Vector3(1f, 1f, 1f);
        m_totalLives += 1;
        m_SoundManager.PlayOneShot(oneUpFX);
    }

    public void MinusLives()
    {
        if (m_totalLives > 0)
        {
            CameraShake.Instance.ShakeCamera(1f, .1f);
            m_SoundManager.PlayOneShot(m_hurtFX);
            m_totalLives -= 1;
            Destroy(this.transform.GetChild(m_totalLives).gameObject);
            Debug.Log(m_totalLives.ToString());
        }
        if (m_totalLives == 0) 
        {
            Gameplay.instance.GameOver();
        }
    }

    public int GetTotalLives()
    {
        return m_totalLives;
    }
}
