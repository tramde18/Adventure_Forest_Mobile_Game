using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public static AIPatrol instance { get; set; }

    [HideInInspector] public bool m_mustPatrol;
    [SerializeField] ParticleSystem dustFX;
    [SerializeField] private AudioClip m_slashFX, m_hurtFX;
    [SerializeField] private float m_walkSpeed;
    [SerializeField] private ParticleSystem m_stunFX;
    public Transform m_groundCheckPos;
    public LayerMask m_groundLayer;
    AudioSource m_SoundManager;
    Animator m_anim;
    bool m_isMovingRight = true;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_SoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();
        m_mustPatrol = true;
    }

    void Update()
    {
        if (m_mustPatrol)
        {
            Patrol();
            
        }
    }


    void Patrol()
    {
        transform.Translate(Vector2.left * m_walkSpeed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(m_groundCheckPos.position, Vector2.down, 2.0f, m_groundLayer);
        if (!groundInfo.collider)
        {
            if (m_isMovingRight)
            {
                Flip(-180f);
                m_isMovingRight = false;
            }
            else
            {
                Flip(0f);
                m_isMovingRight = true;
            }
        }
    }

    void Flip(float _rotation)
    {
        transform.eulerAngles = new Vector3(0, _rotation, 0);
        CreateDust();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_SoundManager.PlayOneShot(m_slashFX);
            CharacterController2D.instance.TakeHit(-transform.right);
            HealthSystem.instance.MinusLives();
            CharacterController2D.instance.ShowBubbleChat("Ouuuch!!!");
        }
    }

    public void CreateDust()
    {
        dustFX.Play();
    }

    public void CreateStun()
    {
        m_stunFX.Play();
    }

    public void TakeHit()
    {
        StartCoroutine(isHit());
    }

    IEnumerator isHit()
    {
        m_mustPatrol = false;
        m_anim.Play("Wolf_Hurt");
        m_SoundManager.PlayOneShot(m_hurtFX);
        CreateDust();
        yield return new WaitForSeconds(1f);
        m_mustPatrol = true;
    }

}
