using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomDoor : MonoBehaviour
{
    public GameObject Target;
    public float Speed;
    private bool _activated;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;

    public ActivatorBase Activator;
    public ActivatorBase PlayerDetector;
    
    public AudioSource AudioSource;
    private bool _isAudioSourceNull;

    protected void Awake()
    {
        _isAudioSourceNull = AudioSource == null;
        _activated = false;
        _targetPosition = Target.transform.position;
        _startPosition = transform.position;
        if (!_isAudioSourceNull && !AudioSource.mute)
        {
            AudioSource.mute = true;
        }
    }

    // Checks every activator in the array to see if they are active
    // If all are active, go to Activation();
    protected void Update()
    {
        if(!_isAudioSourceNull)
        {
            if (TimerManager.Instance.ScaledGameTime > 10f && AudioSource.mute)
            {
                AudioSource.mute = false;
            }
        }
        if (Activator.Active && PlayerDetector.Active && !_activated || !PlayerDetector.Active && !_activated)
        {
            Activation();
            if (!_isAudioSourceNull && !AudioSource.isPlaying)
            {
                AudioSource.Play();
            }
        }

        if (PlayerDetector.Active && !Activator.Active && _activated)
        {
            Deactivation();
            if (!_isAudioSourceNull && !AudioSource.isPlaying)
            {
                AudioSource.Play();
            }
        }
    }

    // When all activators are active, the door moves to its targetPosition
    // When targetPosition is reached, the bool _activated is set to true
    public void Activation()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * TimerManager.Instance.GameDeltaTime);
        if (transform.position == _targetPosition)
        {
            _activated = true;
        }
    }

    protected void Deactivation()
    {
        transform.position = Vector3.MoveTowards(transform.position, _startPosition, Speed * TimerManager.Instance.GameDeltaTime);
        if (transform.position == _startPosition)
        {
            _activated = false;
        }
    }
    
    protected void ResetDefaults()
    {
        _activated = false;
        transform.position = _startPosition;
    }
}
