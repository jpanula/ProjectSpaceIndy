using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMover : MonoBehaviour, IMover, IDamageReceiver
{
    public float Speed { get; set; }
    public Vector3 MovementVector { get; set; }
    public State CurrentState;
    public float SearchRadius;
    public float TurnSpeed;
    public GameObject ProjectileModel;
    public Health Health;
    public Projectile Projectile;
    public float SpawnInvulnerabilityTime;
    private Transform _target;
    private Collider[] _colliders;
    private float _invulnerabilityTimer;

    public enum State
    {
        Launching,
        Searching,
        PlayerFound
        
    }

    private void Awake()
    {
        Health = GetComponent<Health>();
        Projectile = GetComponent<Projectile>();
    }

    private void Start()
    {
        Health.IsInvulnerable = true;
    }

    public void ResetMover()
    {
        CurrentState = State.Launching;
        Health.IsInvulnerable = true;
        _invulnerabilityTimer = 0;
    }

    private void Update()
    {
        _invulnerabilityTimer += TimerManager.Instance.GameDeltaTime;
        if (_invulnerabilityTimer >= SpawnInvulnerabilityTime)
        {
            Health.IsInvulnerable = false;
        }
        transform.rotation = Quaternion.LookRotation(MovementVector);
    }

    private void FixedUpdate()
    {
        var position = transform.position;
        
        switch (CurrentState)
        {
            case State.Launching:
                if (transform.position.y <= 0)
                {
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                    MovementVector = new Vector3(MovementVector.x, 0, MovementVector.z);
                    CurrentState = State.Searching;
                }
                
                break;
            
            case State.Searching:

                _colliders = Physics.OverlapSphere(position, SearchRadius, (int) Const.Layers.Player);
                if (_colliders.Length > 0)
                {
                    _target = _colliders[0].transform;
                    CurrentState = State.PlayerFound;
                }
                
                break;
            case State.PlayerFound:

                var targetPos = _target.position;
                var directionToTarget = targetPos - position;
                var newMovementVector = Vector3.Slerp(MovementVector.normalized, directionToTarget.normalized, TimerManager.Instance.GameDeltaTime * TurnSpeed);
                newMovementVector.y = 0;
                MovementVector = newMovementVector;
                
                if (Vector3.Distance(targetPos, position) > SearchRadius)
                {
                    CurrentState = State.Searching;
                }
                
                break;
            default:
                
                throw new ArgumentOutOfRangeException();
        }
        
        transform.position += MovementVector * Speed * TimerManager.Instance.GameDeltaTime;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SearchRadius);
    }

    public bool TakeDamage(int amount)
    {
       bool died = Health.DecreaseHealth(amount);
       if (died)
       {
           Projectile.ReturnProjectile();
       }

       return died;
    }
}
