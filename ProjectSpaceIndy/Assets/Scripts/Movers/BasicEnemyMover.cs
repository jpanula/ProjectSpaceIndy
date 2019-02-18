using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMover : MonoBehaviour, IMover
{
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public Vector3 MovementVector { get; set; }
    public float TurnSpeed;

    public float _speed;
    public void Reset()
    {
        Speed = 0;
        MovementVector = Vector3.zero;
    }

    private void Update()
    {
        Quaternion lookRotation = Quaternion.LookRotation(MovementVector);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed);
        Vector3 newPos = transform.position + transform.forward * Speed;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
    }
}
