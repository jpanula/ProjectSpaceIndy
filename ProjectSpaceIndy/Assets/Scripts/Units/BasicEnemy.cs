using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicEnemy : UnitBase
{
    [Tooltip("Radius in units where the enemy tries to detect the player")]
    public float DetectionRadius;
    [Tooltip("The layers that are able to block the enemy's vision of the player")]
    public LayerMask VisionBlockedBy = (int) Const.Layers.Environment;
    [Tooltip("Distance in units at which the enemy tries to stay at from player")]
    public float DistanceFromPlayer;
    [Tooltip("Path to use when in patrol mode")]
    public Path PatrolPath;

    [Tooltip("Distance from current target node when the next node should be targeted")]
    public float NodeDistance;

    public State CurrentState;
    private Collider[] _colliders;
    private Transform _target;
    private float _movementSpeed;
    private Node _currentNode;

    public enum State
    {
        Patrol,
        PlayerFound
    }
    
    public override void Awake()
    {
        Health = gameObject.GetOrAddComponent<Health>();
        Mover = gameObject.GetOrAddComponent<BasicEnemyMover>();
        _movementSpeed = Mover.Speed;
        Mover.Speed = 0;
    }

    protected override void Update()
    {
        var position = transform.position;

        switch (CurrentState)
        {
            case State.Patrol:
                if (PatrolPath == null)
                {
                    Mover.Speed = 0;
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
                    Mover.Speed = _movementSpeed;
                }
                
                // Check for players
                _colliders = Physics.OverlapSphere(position, DetectionRadius, (int) Const.Layers.Player);
                int size = _colliders.Length;

                if (size > 0)
                {
                    _target = _colliders[0].transform;
                    CurrentState = State.PlayerFound;
                }
                
                break;
            
            case State.PlayerFound:
                
                var targetPos = _target.position;
                Vector3 playerDirection = targetPos - position;
                Mover.MovementVector = playerDirection;
                var distance = Vector3.Distance(position, targetPos);
            
                // If player is in line of sight, execute the following
                if (!Physics.Raycast(position, playerDirection, distance, VisionBlockedBy))
                {
                    // If player is not at the wanted distance, move appropriately
                    if (DistanceFromPlayer < distance)
                    {
                        Mover.Speed = _movementSpeed;
                    }
                    else
                    {
                        Mover.Speed = 0;
                    }
                    Fire();
                }
                else
                {
                    Mover.Speed = 0;
                }

                if (distance > DetectionRadius)
                {
                    CurrentState = State.Patrol;
                }
                
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
    }
}
