using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadzoneAdjuster : MonoBehaviour
{
    public Slider LeftStickSlider;
    public Slider RightStickSlider;
    public TMP_InputField LeftStickField;
    public TMP_InputField RightStickField;

    private void Start()
    {
        LeftStickSlider.value = LeftDeadzone;
        RightStickSlider.value = RightDeadzone;
        LeftStickField.text = (LeftDeadzone * 1000).ToString("0");
        RightStickField.text = (RightDeadzone * 1000).ToString("0");
    }

    public void UpdateFields()
    {
        LeftStickField.text = (LeftDeadzone * 1000).ToString("0");
        RightStickField.text = (RightDeadzone * 1000).ToString("0");
    }

    public void UpdateSliders()
    {
        LeftStickSlider.value = LeftDeadzone;
        RightStickSlider.value = RightDeadzone;
    }
    
    public float LeftDeadzone
    {
        get { return InputManager.Instance.LeftStickDeadzone; }
        set { InputManager.Instance.LeftStickDeadzone = value; }
    }

    public float RightDeadzone
    {
        get { return InputManager.Instance.RightStickDeadzone; }
        set { InputManager.Instance.RightStickDeadzone = value; }
    }

    public void ResetStickDeadzones()
    {
        InputManager.Instance.ResetDeadzones();
    }

    public void LeftDeadzoneField(String text)
    {
        float newValue = LeftDeadzone;
        if (ParseField(text, out newValue))
        {
            LeftDeadzone = newValue / 1000;
        }
    }
    
    public void RightDeadzoneField(String text)
    {
        float newValue = RightDeadzone;
        if (ParseField(text, out newValue))
        {
            RightDeadzone = newValue / 1000;
        }
    }

    public bool ParseField(string text, out float number)
    {
        return float.TryParse(text, out number);
    }
}
