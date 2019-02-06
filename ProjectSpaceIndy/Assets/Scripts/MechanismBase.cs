using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MechanismBase : MonoBehaviour
{
    public Activator[] Activators;
    private bool _activation;

    public bool Activation
    {
        get { return _activation; }
    }

    private void Awake()
    {
        _activation = false;
    }

    void Update()
    {
        int activeCounter = 0;
        foreach (Activator activator in Activators)
        {
            if (activator.Active)
            {
                activeCounter += 1;
            }
        }

        if (activeCounter == Activators.Length)
        {
            _activation = true;
        }
    }
}
