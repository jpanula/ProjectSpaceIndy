using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : Activator
{
    public Material Green;
    public Renderer Renderer;

    private void OnTriggerEnter(Collider other)
    {
        Renderer.material = Green;
        Active = true;
    }
}
