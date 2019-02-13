using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : ActivatorBase
{
    public Vector3 Dimensions;
    private LayerMask _layer = (int) Const.Layers.Enemy;
    private bool _active;

    public override bool Active
    {
        get { return _active; }
    }

    private void Update()
    {
        _active = Physics.CheckBox(transform.position, Dimensions / 2, Quaternion.identity, _layer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, Dimensions);
    }
}
