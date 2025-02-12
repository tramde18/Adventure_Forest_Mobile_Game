using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherController : MonoBehaviour
{
	public static MotherController instance { get; set; }
    [SerializeField] private GameObject m_chatBubble;                           // Chat Head bubble for player for interaction


	void Awake()
	{
		instance = this;	
	}

	public void ShowBubbleChat(string _text)
	{
		if (gameObject.activeSelf)
		{
			StartCoroutine(_showBubbleChatWait(_text));
		}
	}
	IEnumerator _showBubbleChatWait(string _text)
	{
		m_chatBubble.SetActive(true);
		m_chatBubble.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = _text;
		yield return new WaitForSeconds(2f);
		m_chatBubble.SetActive(false);
	}
}
