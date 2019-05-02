using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayActiveDoor : MechanismBase
{
    private bool _activated;
    private bool _finished;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    
    public AudioSource AudioSource;
    private float _volume;
    

    private void Awake()
    {
        _activated = false;
        _finished = true;
        _targetPosition = Target.transform.position;
        _startPosition = transform.position;
    }

    private void Start()
    {
        if (AudioSource != null)
        {
            _volume = AudioSource.volume;
        }
    }

    // Checks every activator in the array to see if they are active
    // If all are active, go to Activation();
    void Update()
    {
        int activeCounter = 0;
        foreach (ActivatorBase activator in Activators)
        {
            if (activator.Active)
            {
                activeCounter += 1;
            }

            if (!activator.Active && activeCounter > 0)
            {
                activeCounter -= 1;
            }
        }

        if (activeCounter == Activators.Length && !_activated || !_activated && !_finished)
        {
            _finished = false;
            if (AudioSource != null && !AudioSource.isPlaying && !_finished)
            {
                AudioSource.volume = _volume * AudioManager.EffectsVolume;
                AudioSource.Play();
            }
            Activation();
        }
    }

    // When all activators are active, the door moves to its targetPosition
    // When targetPosition is reached, the bool _activated is set to true
    public override void Activation()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * TimerManager.Instance.GameDeltaTime);
        if (transform.position == _targetPosition)
        {
            _activated = true;
            _finished = true;
        }
    }
    
    protected override void ResetDefaults()
    {
        _activated = false;
        _finished = true;
        transform.position = _startPosition;
    }
}
