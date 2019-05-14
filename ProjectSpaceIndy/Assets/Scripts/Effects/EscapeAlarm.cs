using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeAlarm : MonoBehaviour
{
    public AudioSource AudioSource;
    private bool _isAudioNull;
    
    void Start()
    {
        _isAudioNull = AudioSource == null;
    }


    void Update()
    {
        if (GameManager.EscapePhase && !_isAudioNull && !AudioSource.isPlaying)
        {
            AudioSource.Play();
        }
    }
}
