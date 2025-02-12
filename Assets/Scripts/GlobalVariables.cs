using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables instance { get; set; }

    public bool _isAudioOn = true;
    public bool _isGameStarted = false;

    void Awake()
    {
        instance = this;
    }
}
