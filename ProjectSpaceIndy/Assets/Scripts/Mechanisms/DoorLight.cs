using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLight : MonoBehaviour
{
    public Material Green;
    private Renderer _renderer;
    public ActivatorBase activator;
    private bool _active;

    private void Awake()
    {
        _active = false;
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (activator.Active && !_active)
        {
            _renderer.material = Green;
            _active = true;
        }
    }
}
