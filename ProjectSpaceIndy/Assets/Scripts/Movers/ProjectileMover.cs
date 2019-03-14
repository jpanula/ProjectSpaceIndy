using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour, IMover
{

    public Vector3 MovementVector { get; set; }

    public float Speed { get; set; }

    private void FixedUpdate()
    {
        transform.position += MovementVector * Speed * Time.deltaTime;
    }

    public void Reset()
    {
        
    }
}
