using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : ActivatorBase
{
    private bool _active;

    public override bool Active
    {
        get { return _active; }
    }

    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + GetComponent<BoxCollider>().center, Vector3.Scale(transform.localScale, GetComponent<BoxCollider>().size));
    }
}
