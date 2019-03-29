using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour, IMover
{
    public float _speed;
    public float TurnSmoothing;
    public float MoveSmoothing;
    public GameObject Player;
    public float StrafeTime;
    public float WaitTime;
    public float Distance;
    public bool MoveLeft;
    public bool Waiting;
    private float _strafeTimer;
    private float _waitTimer;
    public Vector3 _movementVector;

    public Vector3 MovementVector
    {
        get { return _movementVector; }
        set { _movementVector = value; }
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        _strafeTimer = 0;
        _waitTimer = 0;
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public void Move(Vector3 movementVector)
    {
        Quaternion lookRotation;
        if (Player != null)
        {
            lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);
        }
        else lookRotation = transform.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1 / TurnSmoothing * TimerManager.Instance.GameDeltaTime);

        transform.position =
            Vector3.Lerp(transform.position, transform.position + movementVector * _speed, 1 / MoveSmoothing * TimerManager.Instance.GameDeltaTime);
    }

    private void Update()
    {
        _movementVector = Vector3.zero;
        if (Waiting)
        {
            _waitTimer += TimerManager.Instance.GameDeltaTime;
            Move(_movementVector * TimerManager.Instance.GameDeltaTime);
            if (_waitTimer >= WaitTime)
            {
                Waiting = false;
                _waitTimer = 0;
            }
        }
        else
        {
            _strafeTimer += TimerManager.Instance.GameDeltaTime;
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

            if (Player != null)
            {
                Vector3 playerVector = Vector3.Normalize(Player.transform.position - transform.position);
                float distanceFromPlayer = Vector3.Distance(Player.transform.position, transform.position);
                playerVector *= (distanceFromPlayer - Distance) / Distance;
                Move((_movementVector + playerVector) * TimerManager.Instance.GameDeltaTime);
            }
        }
    }

    public void ResetMover()
    {
        _strafeTimer = 0;
        _waitTimer = 0;
        MoveLeft = false;
        Waiting = false;
    }
}
