using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    [SerializeField, Range(0.0f, 1.0f),Tooltip("The deadzone for the left stick in the gamepad")]
    private float _leftStickDeadzone;
    [SerializeField, Range(0.0f, 1.0f),Tooltip("The deadzone for the right stick in the gamepad")]
    private float _rightStickDeadzone;

    public float LeftStickDeadzone
    {
        get { return _leftStickDeadzone; }
        set { _leftStickDeadzone = Mathf.Clamp01(value); }
    }

    public float RightStickDeadzone
    {
        get { return _rightStickDeadzone; }
        set { _rightStickDeadzone = Mathf.Clamp01(value); }
    }
}
