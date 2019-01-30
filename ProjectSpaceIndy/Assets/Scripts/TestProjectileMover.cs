using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectileMover : MonoBehaviour, IMover
{

    public float _speed;
    public Vector3 _movementVector;

    public Vector3 MovementVector
    {
        get { return _movementVector; }
        set { _movementVector = value; }
    }

    public float Speed
    {
        get
        {
            return _speed;
        }
    }

    public void Move(Vector3 movementVector)
    {
        transform.Translate(movementVector * Speed);
    }

    private void Update()
    {
        Move(MovementVector);
    }
}
