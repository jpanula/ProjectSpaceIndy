using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : ActivatorBase
{
    public Material Green;
    public Renderer Renderer;
    private bool _active;

    private void OnTriggerEnter(Collider other)
    {
        Renderer.material = Green;
        _active = true;
    }

    public override bool Active
    {
        get { return _active; }
        set { _active = value; }
    }
}
