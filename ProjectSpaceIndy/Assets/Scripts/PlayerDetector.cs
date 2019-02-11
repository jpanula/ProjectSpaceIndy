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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _active = true;
            Debug.Log("Inside");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _active = false;
            Debug.Log("Outside");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
    }
}
