using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MechanismBase
{
    private bool _activated;
    public float Speed;
    public GameObject Target;
    private Vector3 targetPosition;

    private void Awake()
    {
        _activated = false;
        targetPosition = Target.transform.position;
    }

    void Update()
    {
        int activeCounter = 0;
        foreach (ActivatorBase activator in Activators)
        {
            if (activator.Active)
            {
                activeCounter += 1;
            }
        }

        if (activeCounter == Activators.Length && !_activated)
        {
            Activation();
        }
    }

    public override void Activation()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            _activated = true;
        }
    }
}
