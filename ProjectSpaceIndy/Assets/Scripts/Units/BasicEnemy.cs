using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : UnitBase
{
    [Tooltip("Radius in units where the enemy tries to detect the player")]
    public float DetectionRadius;
    [Tooltip("Distance in units at which the enemy tries to stay at from player")]
    public float DistanceFromPlayer;
    [Tooltip("The amount of scrap the enemy drops when killed")]
    public int DroppedScrap;
    [Tooltip("The distance at which to drop the scrap")]
    public float ScrapDropDistance;

    private bool _playerFound;
    private Collider[] _colliders;
    private Transform _target;
    private float _movementSpeed;

    public override void Awake()
    {
        Health = gameObject.GetOrAddComponent<Health>();
        Mover = gameObject.GetOrAddComponent<BasicEnemyMover>();
        _movementSpeed = Mover.Speed;
        Mover.Speed = 0;
    }

    protected override void Update()
    {
        // Check for players
        _colliders = Physics.OverlapSphere(transform.position, DetectionRadius, (int) Const.Layers.Player);
        int size = _colliders.Length;
        
        // If multiple players found, find the closest one and target it
        if (size > 1)
        {
            _playerFound = true;
            float distance = Vector3.Distance(_colliders[0].transform.position, transform.position);
            _target = _colliders[0].transform;
            for (int i = 1; i < size; i++)
            {
                float newDistance = Vector3.Distance(_colliders[i].transform.position, transform.position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    _target = _colliders[i].transform;
                }
            }
        }
        // If only one player found, target it
        else if (size == 1)
        {
            _target = _colliders[0].transform;
            _playerFound = true;
        }
        // If no player found, make target null
        else
        {
            _target = null;
            _playerFound = false;
            Mover.Speed = 0;
        }

        if (_playerFound)
        {
            Vector3 playerDirection = _target.position - transform.position;
            Mover.MovementVector = playerDirection;
            float maxDistance = Vector3.Distance(transform.position, _target.position);
            
            // If player is in line of sight, execute the following
            if (!Physics.Raycast(transform.position, playerDirection, maxDistance, (int) Const.Layers.Environment))
            {
                // If player is not at the wanted distance, move appropriately
                if (DistanceFromPlayer < Vector3.Distance(transform.position, _target.position))
                {
                    Mover.Speed = _movementSpeed;
                }
                else
                {
                    Mover.Speed = 0;
                }

                // Shoot at player
                Fire();
            }
            else
            {
                Mover.Speed = 0;
            }
        }
    }
    
    protected override void Die()
    {
        List<PickupBase> scraps = new List<PickupBase>();
        for (int i = 0; i < DroppedScrap; i++)
        {
            scraps.Add(PickupManager.Instance.GetScrap());
        }
        Vector3 dropAngle = Vector3.forward * ScrapDropDistance;
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
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
    }
}
