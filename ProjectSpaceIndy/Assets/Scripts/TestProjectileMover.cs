using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectileMover : MonoBehaviour, IMover
{

    public float _speed;

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
}
