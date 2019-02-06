using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour, IActivator
{
    private bool _active;

    public bool Active
    {
        get { return _active; }
    }
}
