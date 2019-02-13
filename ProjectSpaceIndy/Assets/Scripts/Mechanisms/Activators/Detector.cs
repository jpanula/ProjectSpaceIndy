using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : ActivatorBase
{
    public Vector3 Dimensions;
    public LayerMask LayerMask;
    public Color GizmoColor = new Color(1, 1, 1, 1);
    private bool _active;

    public override bool Active
    {
        get { return _active; }
    }

    private void Update()
    {
        _active = Physics.CheckBox(transform.position, Dimensions / 2, transform.rotation, LayerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Dimensions);
    }
}
