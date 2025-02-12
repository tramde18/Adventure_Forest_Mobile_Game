using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    GameObject m_player;

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            HealthSystem.instance.MinusLives();
            var forceDir = CharacterController2D.instance.GetOppositeDir();
            CharacterController2D.instance.TakeHit(forceDir);
            CharacterController2D.instance.ShowBubbleChat("Ouuuch!!!");
        }
    }
}
