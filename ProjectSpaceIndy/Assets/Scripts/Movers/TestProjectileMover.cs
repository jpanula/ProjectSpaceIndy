using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectileMover : MonoBehaviour, IMover
{

    public Vector3 MovementVector { get; set; }

    public float Speed { get; set; }

    public void Move(Vector3 movementVector)
    {
        transform.position += movementVector * Speed;
    }

    private void FixedUpdate()
    {
        Move(MovementVector);
    }
}
