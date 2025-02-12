using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public static Gameplay instance;
    public Animator m_HUD_Panel;
    public Animator m_Pause_Panel;
    public Animator m_GameOver_Panel;
    public Animator m_Congrats_Panel;
    public AudioClip m_GameOver_FX;
    public AudioClip m_Button_Click_FX;
    public AudioClip m_Congrats_FX;
    public AudioSource m_Background_FX;
    public AudioSource m_Sound_Manager;
    public ParticleSystem m_congratsPS;
    public Text m_MyTimeUI;
    private GameObject m_player;
    private float m_StartTime;

    private void Awake()
    {
        instance = this;
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        if (GlobalVariables.instance._isGameStarted)
        {
            Timer.instance.StartTimer();
            m_StartTime = Timer.instance.m_timeRemaining;
        }
    }

    void Update()
    {
        if (GlobalVariables.instance._isAudioOn)
        {
            foreach (Transform child in m_Sound_Manager.transform)
            {
                child.GetComponent<AudioSource>().mute = false;
            }
            m_Sound_Manager.GetComponent<AudioSource>().mute = false;
            //m_Sound_Manager.gameObject.SetActive(true);
        }
        else
        {
            foreach (Transform child in m_Sound_Manager.transform)
            {
                child.GetComponent<AudioSource>().mute = true;
            }
            m_Sound_Manager.GetComponent<AudioSource>().mute = true;
            //m_Sound_Manager.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        m_Sound_Manager.PlayOneShot(m_Button_Click_FX);
        Transform cutscenesCamTrans = GameObject.FindGameObjectWithTag("CutscenesCamera").transform;
        Camera.main.transform.position = cutscenesCamTrans.position;
        GlobalVariables.instance._isGameStarted = true;
        StartCoroutine(DialogController.instance.Type());
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("GlobalVariables"));
        //StartCoroutine(LoadSceneAsync("Gameplay"));
    }

    public void PauseGame(bool _status)
    {
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("GlobalVariables"));
        m_Sound_Manager.PlayOneShot(m_Button_Click_FX);
        if (_status)
        {
            m_HUD_Panel.Play("HUDPanel_Idle");
            m_Pause_Panel.Play("PausePanel_PopUp");
            Timer.instance.m_timerIsRunning = false;
            Time.timeScale = 0f;
            m_Background_FX.Pause();
        }
        else
        {
            m_HUD_Panel.Play("HUDPanel_PopUp");
            m_Pause_Panel.Play("PausePanel_Idle");
            Timer.instance.m_timerIsRunning = true;
            Time.timeScale = 1f;
            m_Background_FX.Play();
        }
    }

    public void RestartGame()
    {
        DontDestroyOnLoad(GlobalVariables.instance.gameObject);
        m_Sound_Manager.PlayOneShot(m_Button_Click_FX);
        StartCoroutine(LoadSceneAsync("Gameplay"));
    }

    public void QuitGame()
    {
        DontDestroyOnLoad(GlobalVariables.instance.gameObject);
        GlobalVariables.instance._isGameStarted = false;
        m_Sound_Manager.PlayOneShot(m_Button_Click_FX);
        StartCoroutine(LoadSceneAsync("MainMenu"));

    }

    public void QuitApplication()
    {
        m_Sound_Manager.PlayOneShot(m_Button_Click_FX);
        Application.Quit();
    }

    public void GameOver()
    {
        StartCoroutine(_GameOver());
    }

    IEnumerator _GameOver()
    {
        m_player.SetActive(false);
        m_Background_FX.Pause();
        m_Sound_Manager.PlayOneShot(m_GameOver_FX);
        m_HUD_Panel.Play("HUDPanel_Idle");
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
        m_GameOver_Panel.Play("GameOverPanel_PopUp");
        Timer.instance.m_timerIsRunning = false;
    }


    IEnumerator LoadSceneAsync(string _sceneName)
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(_sceneName);
    }

    public void AudioOnOff(bool _isOn)
    {
        StartCoroutine(_AudioOnOff(_isOn));
    }

    IEnumerator _AudioOnOff(bool _isOn)
    {
        if (_isOn)
        {
            GlobalVariables.instance._isAudioOn = true;
            m_Sound_Manager.PlayOneShot(m_Button_Click_FX);
        }
        else
        {
            m_Sound_Manager.PlayOneShot(m_Button_Click_FX);
            yield return new WaitForSeconds(0.2f);
            GlobalVariables.instance._isAudioOn = false;
        }
    }

    public void PlaySoundFX(AudioClip _soundFX)
    {
        m_Sound_Manager.PlayOneShot(_soundFX);
    }

    public void Congrats()
    {
        m_HUD_Panel.Play("HUDPanel_Idle");
        Timer.instance.m_timerIsRunning = false;
        float myTime = m_StartTime - Timer.instance.m_timeRemaining;
        float minutes = Mathf.FloorToInt(myTime / 60);
        float seconds = Mathf.FloorToInt(myTime % 60);
        
        string timeToDisplay = string.Format("{0:00}:{1:00}", minutes, seconds);
        m_MyTimeUI.text = "Time - " + timeToDisplay;
        m_Background_FX.gameObject.SetActive(false);

        m_Sound_Manager.PlayOneShot(m_Congrats_FX);
        m_congratsPS.Play();
        m_Congrats_Panel.Play("CongratsPanel_PopUp");

        float bestTime = PlayerPrefs.GetFloat("BestTime");
        if (bestTime == 0 || myTime < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", myTime);
        }
        
    }

    public void BestTime(Text bestTimeUI)
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime");
        float minutes = Mathf.FloorToInt(bestTime / 60);
        float seconds = Mathf.FloorToInt(bestTime % 60);

        string timeToDisplay = string.Format("{0:00}:{1:00}", minutes, seconds);
        bestTimeUI.text = "Time: " + timeToDisplay;
    }

}
