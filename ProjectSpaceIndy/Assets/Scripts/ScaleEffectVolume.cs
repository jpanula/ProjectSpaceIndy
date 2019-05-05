using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEffectVolume : MonoBehaviour
{
    public AudioSource AudioSource;
    private float _volume;

    private void Awake()
    {
        if (AudioSource == null)
        {
            AudioSource = GetComponent<AudioSource>();
        }

        if (AudioSource != null)
        {
            _volume = AudioSource.volume;
            AudioSource.volume = _volume * AudioManager.EffectsVolume;
        }
    }

    private void Update()
    {
        AudioSource.volume = _volume * AudioManager.EffectsVolume;
    }
}
