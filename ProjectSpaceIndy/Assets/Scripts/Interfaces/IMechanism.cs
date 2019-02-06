using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMechanism
{
    IActivator[] Activators { get; }

    void Activation();
}
