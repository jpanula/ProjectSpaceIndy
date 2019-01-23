using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour, IMover
{
    public float _speed;
    public float TurnSmoothing;
    public float MoveSmoothing;
    public GameObject Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public void Move(Vector3 movementVector)
    {
        Quaternion lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1 / TurnSmoothing * Time.deltaTime);

        transform.position =
            Vector3.Lerp(transform.position, transform.position + movementVector * _speed, 1 / MoveSmoothing * Time.deltaTime);
    }
}
