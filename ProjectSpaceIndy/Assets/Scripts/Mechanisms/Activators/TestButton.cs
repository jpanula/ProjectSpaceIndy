using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestButton : ActivatorBase
{
    public Material Green;
    private Renderer _renderer;
    private bool _active;
    private bool _activated;
    public float MaxDistance = 20;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if (MaxDistance < 1)
        {
            MaxDistance = 20;
        }
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
        set
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (Vector3.Distance(player.transform.position, transform.position) <= MaxDistance)
            {
                _active = value;  
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, MaxDistance);
    }
}
