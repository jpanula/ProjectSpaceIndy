using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : ActivatorBase
{
    public Material Green;
    private Renderer _renderer;
    private bool _active;
    private bool _activated;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (_active && !_activated)
        {
            _renderer.material = Green;
            _activated = true;
        }
    }

    public override bool Active
    {
        get { return _active; }
        set { _active = value; }
    }
}
