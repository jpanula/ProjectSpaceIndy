using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : ActivatorBase
{
    public Vector3 Dimensions;
    public LayerMask LayerMask;
    public float Delay;
    public float Cooldown;
    public Color GizmoColor = new Color(1, 1, 1, 1);
    private bool _active;
    private float _cooldownTimer;
    private float _delayTimer;
    private bool _waitingForDelay;

    public override bool Active
    {
        get { return _active; }
        set { _active = value; }
    }

    private void Update()
    {
        
        if (Physics.CheckBox(transform.position, Dimensions / 2, transform.rotation, LayerMask))
        {
            _waitingForDelay = true;
            _cooldownTimer = 0;
        }
        else
        {
            if (_cooldownTimer >= Cooldown)
            {
                _active = false;
                _delayTimer = 0;
            }
        }

        if (_waitingForDelay)
        {
            _delayTimer += Time.deltaTime;
            if (_delayTimer >= Delay)
            {
                _active = true;
                _waitingForDelay = false;
            }
        }
        else
        {
            _cooldownTimer += Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Dimensions);
    }
}
