using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomDoor : MonoBehaviour
{
    public GameObject Target;
    public float Speed;
    private bool _activated;
    private bool _finished;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;

    public ActivatorBase Activator;
    public ActivatorBase PlayerDetector;
    

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
        if (Activator.Active && PlayerDetector.Active && !_activated || !PlayerDetector.Active && !_activated)
        {
            Activation();
        }

        if (PlayerDetector.Active && !Activator.Active && _activated)
        {
            Deactivation();
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
    
    protected void ResetDefaults()
    {
        _activated = false;
        _finished = true;
        transform.position = _startPosition;
    }
}
