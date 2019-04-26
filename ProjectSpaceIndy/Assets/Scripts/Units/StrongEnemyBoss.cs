using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public float TeleportHPTreshold;
    public float TeleportCoolDown;
    public float TeleportDistanceFromPlayer;
    public ParticleSystem TeleportEffect;

    private GameObject _target;
    private Collider[] _colliders;
    private bool _targetAcquired;
    private Node _currentNode;
    private float _teleportTimer;
    private SphereCollider _ownCollider;
    
    public enum State
    {
        Patrol = 0,
        PlayerDetected = 1
    }

    public override void Awake()
    {
        _ownCollider = GetComponent<SphereCollider>();
        base.Awake();
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
                _teleportTimer = 0;
                
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
                
                if ((float) Health.CurrentHealth / Health.MaxHealth <= TeleportHPTreshold)
                {
                    _teleportTimer += TimerManager.Instance.GameDeltaTime;
                    if (_teleportTimer >= TeleportCoolDown)
                    {
                        _teleportTimer = 0;
                        Teleport();
                    }
                }
                
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

    private void Teleport()
    {
        var t = transform;
        var teleportEffectObject = Instantiate(TeleportEffect.gameObject, t.position, t.rotation);
        var main = teleportEffectObject.GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Destroy;
        main.simulationSpeed = TimerManager.Instance.GameDeltaScale;
        
        LayerMask layerMask = (int) (Const.Layers.Enemy | Const.Layers.Activator | Const.Layers.Environment |
                                     Const.Layers.Hazard | Const.Layers.EnemyBarrier | Const.Layers.InvisibleWall);
        var targetPos = _target.transform.position;
        var targetCollider = _target.GetComponent<SphereCollider>();
        Vector3 newPos;
        do
        {
            newPos = Random.insideUnitCircle;
            newPos = newPos.normalized;
            newPos *= targetCollider.radius + _ownCollider.radius + TeleportDistanceFromPlayer;
            newPos.z = newPos.y;
            newPos.y = 0;
            newPos += targetPos;
        } while (Physics.CheckSphere(newPos, _ownCollider.radius, layerMask) || Physics.Raycast(newPos, targetPos - newPos, Vector3.Distance(newPos, targetPos), layerMask));
        
        transform.position = newPos;
        transform.LookAt(targetPos);
        
        teleportEffectObject = Instantiate(TeleportEffect.gameObject, t.position, t.rotation);
        main = teleportEffectObject.GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Destroy;
        main.simulationSpeed = TimerManager.Instance.GameDeltaScale;
    }
}
