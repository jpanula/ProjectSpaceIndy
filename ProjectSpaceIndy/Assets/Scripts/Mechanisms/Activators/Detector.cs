using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : ActivatorBase
{
    public Vector3 Dimensions;
    public LayerMask LayerMask;
    public float Cooldown;
    public Color GizmoColor = new Color(1, 1, 1, 1);
    private bool _active;
    private float _cooldownTimer;

    public override bool Active
    {
        get { return _active; }
        set { _active = value; }
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (Physics.CheckBox(transform.position, Dimensions / 2, transform.rotation, LayerMask))
        {
            _active = true;
            _cooldownTimer = 0;
        }
        else
        {
            if (_cooldownTimer > Cooldown)
            {
                _active = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Dimensions);
    }
}
