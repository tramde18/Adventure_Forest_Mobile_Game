using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    public static DialogController instance { get; set; }
    public TextMeshProUGUI textDisplay;
    public GameObject continueBtn;
    public GameObject menuCanvas;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    public float m_HorizontalMove;
    public AudioClip nightAtmosphereFX;
    public AudioSource backgroundSourceFX;
    bool m_isWalking = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {

        if (textDisplay.text == sentences[index])
        {
            continueBtn.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (m_isWalking)
        {
            CharacterController2D.instance.Move(m_HorizontalMove * Time.fixedDeltaTime, false, false);
            if (!CharacterController2D.instance.gameObject.GetComponent<SpriteRenderer>().isVisible)
            {
                DontDestroyOnLoad(GlobalVariables.instance.gameObject);
                SceneManager.LoadScene("Gameplay");
            }
        }
    }

    public IEnumerator Type()
    {
        menuCanvas.SetActive(false);
        backgroundSourceFX.clip = nightAtmosphereFX;
        backgroundSourceFX.Play();
        yield return new WaitForSeconds(0.5f);
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueBtn.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueBtn.SetActive(false);
            StartCoroutine(_ConvoDialog());
        }

    }


    IEnumerator _ConvoDialog()
    {
        MotherController.instance.ShowBubbleChat("Remember, go straight to Grandma's house");
        yield return new WaitForSeconds(2f);
        CharacterController2D.instance.ShowBubbleChat("Don't worry, mommy, I'll be careful");
        CharacterController2D.instance.gameObject.GetComponent<Animator>().Play("StandaloneWalk");
        CharacterController2D.instance.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        m_isWalking = true;
    }
}
