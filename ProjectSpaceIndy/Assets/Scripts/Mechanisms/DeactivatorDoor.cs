using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatorDoor : MechanismBase
{
    public ActivatorBase[] Deactivators;
    
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
    
    void Update()
    {
        int activeCounter = 0;
        int deactiveCounter = 0;
        
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

        foreach (ActivatorBase deactivator in Deactivators)
        {
            if (deactivator.Active)
            {
                deactiveCounter += 1;
            }

            if (!deactivator.Active && deactiveCounter > 0)
            {
                deactiveCounter -= 1;
            }
        }

        if ((activeCounter == Activators.Length && deactiveCounter == 0) || !_activated && !_finished)
        {
            _activated = false;
            _finished = false;
            Activation();
        }

        if ((_activated && _finished && activeCounter < Activators.Length) || deactiveCounter == Deactivators.Length)
        {
            Deactivation();
            
        }
    }

    public override void Activation()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * TimerManager.Instance.GameDeltaTime);
        if (transform.position == _targetPosition)
        {
            _activated = true;
            _finished = true;
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
