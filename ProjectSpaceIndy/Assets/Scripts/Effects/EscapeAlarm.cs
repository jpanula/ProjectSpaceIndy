using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeAlarm : MonoBehaviour
{
    public AudioSource AudioSource;
    private bool _isAudioNull;
    private float _volume;
    
    void Start()
    {
        _isAudioNull = AudioSource == null;
        if (!_isAudioNull)
        {
            _volume = AudioSource.volume;
        }
    }


    void Update()
    {
        if (GameManager.EscapePhase && !_isAudioNull && !AudioSource.isPlaying)
        {
            AudioSource.volume = _volume * AudioManager.EffectsVolume;
            AudioSource.Play();
        }
    }
}
