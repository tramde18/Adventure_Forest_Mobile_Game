using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public float m_walkSpeed = 40f;
    public float m_jumpEnemyForce = 1000f;
    float m_horizontalMove, m_horizontalAxis;
    
    bool m_jump = false;
    bool m_isInGround = true;

    private void Awake()
    {
        instance = this;   
    }

    private void Start()
    {
        m_horizontalAxis = Input.GetAxis("Horizontal");
    }

    private void Update()
    {
        m_horizontalMove = m_horizontalAxis * m_walkSpeed;
        m_isInGround = CharacterController2D.instance.GroundCheck();

    }

    private void FixedUpdate()
    {
        CharacterController2D.instance.Move(m_horizontalMove * Time.fixedDeltaTime, false, m_jump);
        m_jump = false;
        
    }

    public void SetHorizontalMovement(float movement)
    {
        m_horizontalAxis = movement;
    }

    public void PlayerJump()
    {
        if (m_isInGround)
        {
            m_jump = true;
        }

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyHeadCollider")
        {
            var AIController = other.transform.parent.GetComponent<AIPatrol>();
            AIController.TakeHit();
            AIController.CreateStun();
            CharacterController2D.instance.Jump(m_jumpEnemyForce);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CliffCollider")
        {
            Gameplay.instance.GameOver();
        }

        if (other.gameObject.tag == "GrandmaHouse")
        {
            other.gameObject.GetComponent<Animator>().Play("GrandMotherHouse_Open");
        }

        if (other.gameObject.tag == "GrandmaHouseDoor")
        {
            Gameplay.instance.Congrats();
            Debug.Log("Congrats!");
        }
    }

}
