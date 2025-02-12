using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float m_length, m_startpos, m_startHeight;
    public GameObject m_cam;
    public float m_parallaxEffect;

    void Start()
    {
        m_startpos = transform.position.x;
        m_startHeight = transform.position.y;
        m_length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (m_cam.transform.position.x * (1 - m_parallaxEffect));
        float distance = (m_cam.transform.position.x * m_parallaxEffect);

        transform.position = new Vector3(m_startpos + distance, m_startHeight, transform.position.z);
        if (temp > m_startpos + m_length)
        {
            m_startpos += m_length;
        }
        else if (temp < m_startpos - m_length)
        {
            m_startpos -= m_length;
        }
    }
}
