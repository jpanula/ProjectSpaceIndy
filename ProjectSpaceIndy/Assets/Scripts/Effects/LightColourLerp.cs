using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColourLerp : MonoBehaviour
{
    public Light DirectionalLight;
    private Color _lerpedColour;
    private float _startTime;
    private bool _firstTime = true;
    
    void Update()
    {
        if (GameManager.EscapePhase)
        {
            if (_firstTime)
            {
                _startTime = TimerManager.Instance.ScaledGameTime;
                _firstTime = false;
            }

            _lerpedColour = Color.Lerp(Color.white, Color.red, Mathf.PingPong(TimerManager.Instance.ScaledGameTime - _startTime, 1));
            DirectionalLight.color = _lerpedColour;
        }
    }
}
