using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    public GameObject Player;
    public float StrafeTime;
    public float WaitTime;
    public float Distance;
    public bool MoveLeft;
    public bool Waiting;
    private float _strafeTimer;
    private float _waitTimer;
    private Vector3 _movementVector;

    private void Start()
    {
        _strafeTimer = 0;
        _waitTimer = 0;
        Player = GameObject.FindWithTag("Player");
    }

    protected override void Update()
    {
        _movementVector = Vector3.zero;
        if (Waiting)
        {
            _waitTimer += Time.deltaTime;
            Mover.Move(_movementVector * Time.deltaTime);
            if (_waitTimer >= WaitTime)
            {
                Waiting = false;
                _waitTimer = 0;
            }
        }
        else
        {
            _strafeTimer += Time.deltaTime;
            if (MoveLeft)
            {
                _movementVector = -transform.right;
            }
            else
            {
                _movementVector = transform.right;
            }

            if (_strafeTimer >= StrafeTime)
            {
                Waiting = true;
                _strafeTimer = 0;
                MoveLeft = !MoveLeft;
            }

            Vector3 playerVector = Vector3.Normalize(Player.transform.position - transform.position);
            float distanceFromPlayer = Vector3.Distance(Player.transform.position, transform.position);
            playerVector *= (distanceFromPlayer - Distance) / Distance;
            Mover.Move((_movementVector + playerVector) * Time.deltaTime);
        }
    }
}
