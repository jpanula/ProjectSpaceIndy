using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioClip : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip[] AudioClips;
    private AudioClip _randomClip;
    private float _volume;
    
    private void Awake()
    {
        if (AudioSource != null)
        {
            _volume = AudioSource.volume;
        }
        int random = Random.Range(0, AudioClips.Length);
        _randomClip = AudioClips[random];
        AudioSource.clip = _randomClip;
        AudioSource.volume = _volume * AudioManager.EffectsVolume;
        AudioSource.Play();
    }
    
    
}
