using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjuster : MonoBehaviour
{
    #region UI elements
    
    public Slider MasterVolumeSlider;
    public Slider EffectsVolumeSlider;
    public Slider MusicVolumeSlider;
    public TMP_InputField MasterVolumeField;
    public TMP_InputField EffectsVolumeField;
    public TMP_InputField MusicVolumeField;
    
    #endregion UI elements
    
    # region Properties
    
    public float MasterVolume
    {
        get { return AudioManager.MasterVolume; }
        set { AudioManager.MasterVolume = value; }
    }

    public float EffectsVolume
    {
        get { return AudioManager.EffectsVolume; }
        set { AudioManager.EffectsVolume = value; }
    }

    public float MusicVolume
    {
        get { return AudioManager.MusicVolume; }
        set { AudioManager.MusicVolume = value; }
    }

    # endregion Properties
    
    public bool ParseField(string text, out float number)
    {
        return float.TryParse(text, out number);
    }
    
    public void ResetVolume()
    {
        AudioManager.ResetVolumeSettings();
    }
    
    public void UpdateFields()
    {
        MasterVolumeField.text = (MasterVolume * 100).ToString("0.0");
        EffectsVolumeField.text = (EffectsVolume * 100).ToString("0.0");
        MusicVolumeField.text = (MusicVolume * 100).ToString("0.0");
    }

    public void UpdateSliders()
    {
        MasterVolumeSlider.value = MasterVolume;
        EffectsVolumeSlider.value = EffectsVolume;
        MusicVolumeSlider.value = MusicVolume;
    }
    
    public void ParseMasterVolumeField(string text)
    {
        float newValue = MasterVolume;
        if (ParseField(text, out newValue))
        {
            MasterVolume = newValue / 100;
        }
    }
    
    public void ParseEffectsVolumeField(string text)
    {
        float newValue = EffectsVolume;
        if (ParseField(text, out newValue))
        {
            EffectsVolume = newValue / 100;
        }
    }
    
    public void ParseMusicVolumeField(string text)
    {
        float newValue = MusicVolume;
        if (ParseField(text, out newValue))
        {
            MusicVolume = newValue / 100;
        }
    }

    private void Start()
    {
        MasterVolumeSlider.value = MasterVolume;
        EffectsVolumeSlider.value = EffectsVolume;
        MusicVolumeSlider.value = MusicVolume;

        MasterVolumeField.text = (MasterVolume * 100).ToString("0.0");
        EffectsVolumeField.text = (EffectsVolume * 100).ToString("0.0");
        MusicVolumeField.text = (MusicVolume * 100).ToString("0.0");
    }
}
