using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacknForthDoor : MechanismBase
{
    private bool _activated;
    private bool _finished;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    

    protected void Awake()
    {
        _activated = false;
        _finished = true;
        _targetPosition = Target.transform.position;
        _startPosition = transform.position;
    }

    // Checks every activator in the array to see if they are active
    // If all are active, go to Activation();
    protected void Update()
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

        if (activeCounter == Activators.Length || !_activated && !_finished)
        {
            _activated = false;
            //_finished = false;
            Activation();
        }

        if (_activated && _finished && activeCounter < Activators.Length)
        {
            Deactivation();
            
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
            //_finished = true;
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
    
    protected override void ResetDefaults()
    {
        _activated = false;
        _finished = true;
        transform.position = _startPosition;
    }
}
