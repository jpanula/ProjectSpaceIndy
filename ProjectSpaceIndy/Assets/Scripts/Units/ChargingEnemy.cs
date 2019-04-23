using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy : UnitBase
{
    public float ChargeTime;
    public float ChargeDistance;
    public float ChargeTimeout;
    public float ChargeMaxAngle;
    public float DistanceFromPlayer;
    public float PlayerDetectionRadius;
    public float ChargeSpeedMultiplier;
    public float TurnSpeed;
    public LayerMask VisionBlockedBy;
    public AfterImage AfterImage;
    public Path PatrolPath;
    public float NodeDistance;
    
    public BoxCollider Bumper;
    public State CurrentState;

    private GameObject _target;
    private Collider[] _colliders;
    private float _chargeTimer;
    private float _baseSpeed;
    private float _chargedDistance;
    private Vector3 _lastPosition;
    private Vector3 _targetPos;

    public AudioSource ChargeSound;
    private Node _currentNode;

    public enum State
    {
        Patrol,
        PlayerDetected,
        Charging,
        ChargeAttack
    }

    public override void Awake()
    {
        base.Awake();
        _baseSpeed = Mover.Speed;
        _targetPos = Vector3.zero;
        AfterImage.enabled = false;
    }

    protected override void Update()
    {
        var t = transform;
        var pos = t.position;
        
        
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

                    if (Vector3.Distance(pos, _currentNode.GetPosition()) < NodeDistance)
                    {
                        _currentNode = PatrolPath.GetNextNode(_currentNode);
                    }

                    var nodeDirection = _currentNode.GetPosition() - pos;
                    Mover.MovementVector = nodeDirection;
                    Mover.Speed = _baseSpeed;
                }
                
                Bumper.gameObject.SetActive(false);

                Mover.MovementVector = Vector3.zero;
                
                _colliders = Physics.OverlapSphere(pos, PlayerDetectionRadius, (int) Const.Layers.Player);
                if (_colliders.Length > 0)
                {
                    var direction = _colliders[0].transform.position - pos;
                    RaycastHit hit;
                    if (!Physics.Raycast(pos, direction, out hit, direction.magnitude, VisionBlockedBy))
                    {
                        _target = _colliders[0].gameObject;
                        _targetPos = _target.transform.position;
                        CurrentState = State.PlayerDetected;
                    }
                }

                break;
            
            case State.PlayerDetected:
                Bumper.gameObject.SetActive(false);
                Mover.Speed = _baseSpeed;

                _targetPos = _target.transform.position;
                var distanceFromPlayer = Vector3.Distance(pos, _targetPos);
                
                if (distanceFromPlayer < DistanceFromPlayer && Vector3.Angle(t.forward, _targetPos - pos) <= ChargeMaxAngle)
                {
                    CurrentState = State.Charging;
                }
                else if (distanceFromPlayer > PlayerDetectionRadius)
                {
                    CurrentState = State.Patrol;
                }
                else
                {
                    var lookRotation = Quaternion.LookRotation(_targetPos - pos, Vector3.up);
                    transform.rotation = Quaternion.Slerp(t.rotation, lookRotation, TimerManager.Instance.GameDeltaTime * TurnSpeed);
                    Mover.MovementVector = t.forward;
                }
                break;
            
            case State.Charging:
                Bumper.gameObject.SetActive(false);
                
                Mover.MovementVector = t.forward;
                Mover.Speed = 0;
                _chargeTimer += TimerManager.Instance.GameDeltaTime;
                
                if (_chargeTimer >= ChargeTime)
                {
                    _lastPosition = pos;
                    CurrentState = State.ChargeAttack;
                    _chargeTimer = 0;
                    /*if (ChargeSound != null && !ChargeSound.isPlaying)
                    {
                        ChargeSound.Play();
                    }*/
                }
                
                break;
            
            case State.ChargeAttack:
                Bumper.gameObject.SetActive(true);
                AfterImage.enabled = true;

                
                Mover.Speed = _baseSpeed * ChargeSpeedMultiplier;
                _chargedDistance += Vector3.Distance(_lastPosition, pos);
                _chargeTimer += TimerManager.Instance.GameDeltaTime;
                _lastPosition = pos;

                if (_chargedDistance >= ChargeDistance || _chargeTimer > ChargeTimeout)
                {
                    _chargedDistance = 0;
                    _chargeTimer = 0;
                    Mover.Speed = _baseSpeed;
                    CurrentState = State.Patrol;
                    AfterImage.enabled = false;
                }
                break;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRadius);
    }
}
