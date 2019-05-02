using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static float MasterVolume
    {
        get { return Instance._masterVolume; }
        set { Instance._masterVolume = Mathf.Clamp01(value); }
    }

    public static float EffectsVolume
    {
        get { return Instance._effectsVolume;}
        set { Instance._effectsVolume = Mathf.Clamp01(value); }
    }

    public static float MusicVolume
    {
        get { return Instance._musicVolume; }
        set { Instance._musicVolume = Mathf.Clamp01(value); }
    }
    
    [SerializeField, Range(0, 1)]
    private float _masterVolume;
    [SerializeField, Range(0, 1)]
    private float _effectsVolume;
    [SerializeField, Range(0, 1)]
    private float _musicVolume;

    public static void ResetVolumeSettings()
    {
        Instance._masterVolume = 0.8f;
        Instance._effectsVolume = 1;
        Instance._musicVolume = 1;
    }
}
