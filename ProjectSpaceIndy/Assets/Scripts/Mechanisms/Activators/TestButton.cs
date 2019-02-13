using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : ActivatorBase
{
    public Material Green;
    public Renderer Renderer;
    private bool _active;
    private bool _activated;

    private void Update()
    {
        if (_active && !_activated)
        {
            Renderer.material = Green;
            _activated = true;
        }
    }

    public override bool Active
    {
        get { return _active; }
        set { _active = value; }
    }
}
