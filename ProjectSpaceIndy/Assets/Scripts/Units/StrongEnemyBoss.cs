using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemyBoss : UnitBase
{
    public Weapon RocketLauncher;
    
    public float DistanceFromPlayer;
    public float PlayerDetectionRadius;
    public LayerMask VisionBlockedBy;
    public State CurrentState = State.Patrol;
    public float TurnSpeed;
    public Path PatrolPath;
    public float NodeDistance;
    public float RocketHPTreshold;

    private GameObject _target;
    private Collider[] _colliders;
    private bool _targetAcquired;
    private Node _currentNode;
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
                if (PatrolPath == null)
                {
                    Mover.MovementVector = Vector3.zero;
                }
                
                else
                {
                    if (_currentNode == null)
                    {
                        _currentNode = PatrolPath.GetClosestNode(transform.position);
                    }

                    if (Vector3.Distance(position, _currentNode.GetPosition()) < NodeDistance)
                    {
                        _currentNode = PatrolPath.GetNextNode(_currentNode);
                    }

                    var nodeDirection = _currentNode.GetPosition() - position;
                    Mover.MovementVector = nodeDirection;
                }
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
                if ((float) Health.CurrentHealth / Health.MaxHealth <= RocketHPTreshold)
                {
                    RocketLauncher.Fire();
                }
                
                break;
            
            default:
                
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRadius);
    }
}
