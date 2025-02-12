using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && HealthSystem.instance.GetTotalLives() < 3)
        {
            HealthSystem.instance.AddLives();
            CharacterController2D.instance.ShowBubbleChat("YEESSSS!!!");
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Player" && HealthSystem.instance.GetTotalLives() == 3)
        {
            CharacterController2D.instance.ShowBubbleChat("Health is full...");
        }
    }
}
