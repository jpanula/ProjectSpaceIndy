using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : UnitBase
{
    public float DistanceFromPlayer;
    public float PlayerDetectionRadius;
    public LayerMask VisionBlockedBy;
    public State CurrentState = State.Patrol;
    public float TurnSpeed;
    [Tooltip("The amount of scrap the enemy drops when killed")]
    public int DroppedScrap;
    [Tooltip("The maximum radius at which to drop the scrap")]
    public float ScrapDropRadius;

    private GameObject _target;
    private Collider[] _colliders;
    private bool _targetAcquired;
    public enum State
    {
        Patrol = 0,
        PlayerDetected = 1
    }
    
    protected override void Update()
    {
        var position = transform.position;
        _colliders = Physics.OverlapSphere(position, PlayerDetectionRadius, (int) Const.Layers.Player);
        if (_colliders.Length > 0)
        {
            var direction = _colliders[0].transform.position - position;
            if (!Physics.Raycast(position, direction, direction.magnitude, VisionBlockedBy))
            {
                _targetAcquired = true;
                _target = _colliders[0].gameObject;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    TimerManager.Instance.GameDeltaTime * TurnSpeed);
            }
            else
            {
                _targetAcquired = false;
            }
        }
        else
        {
            _targetAcquired = false;
        }

        if (_targetAcquired)
        {
            CurrentState = State.PlayerDetected;
        }
        else
        {
            CurrentState = State.Patrol;
        }
        
        switch (CurrentState)
        {
            case State.Patrol:
                Mover.MovementVector = Vector3.zero;
                break;
            
            case State.PlayerDetected:
                
                var targetPos = _target.transform.position;
                if (DistanceFromPlayer > Vector3.Distance(targetPos, position))
                {
                    Mover.MovementVector = -(targetPos - position);
                }
                else if (DistanceFromPlayer < Vector3.Distance(targetPos, position))
                {
                    Mover.MovementVector = targetPos - position;
                }
                
                Fire();
                
                break;
            
            default:
                
                throw new ArgumentOutOfRangeException();
        }
    }
    
    protected override void Die()
    {
        List<PickupBase> scraps = new List<PickupBase>();
        for (int i = 0; i < DroppedScrap; i++)
        {
            scraps.Add(PickupManager.Instance.GetScrap());
        }
        Vector3 dropAngle = Vector3.forward * ScrapDropRadius;
        for (int i = 0; i < scraps.Count; i++)
        {
            if (scraps[i] != null)
            {
                scraps[i].transform.position = dropAngle + transform.position;
                dropAngle = Quaternion.AngleAxis(360.0f / scraps.Count, Vector3.up) * dropAngle;
            }
        }

        PickupBase fuel = PickupManager.Instance.GetFuel();
        if (fuel != null)
        {
            fuel.transform.position = transform.position;
        }
        base.Die();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRadius);
    }
}
