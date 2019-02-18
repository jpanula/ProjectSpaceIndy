using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MechanismBase : MonoBehaviour
{
    public ActivatorBase[] Activators;

    public abstract void Activation();

    protected abstract void ResetDefaults();
}
