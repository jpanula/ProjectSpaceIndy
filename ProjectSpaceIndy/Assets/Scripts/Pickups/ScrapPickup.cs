using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPickup : PickupBase
{
    public int Score;
    public float AttractionRadius;
    public float AttractionSpeed;
    public float DistanceFactor;
    private Transform _target;
    private float _movementTimer;

    protected override void Update()
    {
        base.Update();
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, AttractionRadius, (int) Const.Layers.Player);
        if (colliders.Length > 0)
        {
            _target = colliders[0].transform;
            float distanceToTarget = DistanceToTarget();
            for (int i = 0; i < colliders.Length; i++)
            {
                float distanceToCurrent = Vector3.Distance(transform.position, colliders[i].transform.position);
                if (distanceToCurrent < distanceToTarget)
                {
                    _target = colliders[i].transform;
                    distanceToTarget = distanceToCurrent;
                }
            }
        }

        if (_target != null)
        {
            _movementTimer += Time.deltaTime * AttractionSpeed;
            transform.position = Vector3.Lerp(transform.position, _target.position, _movementTimer);
        }

        
    }
    
    protected override void GrantEffect(PlayerUnit playerUnit)
    {
        GameManager.Score += Score;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        if (player != null)
        {
            GrantEffect(player);
            Reset();
            PickupManager.Instance.ReturnScrap(this);
        }
    }

    private float DistanceToTarget()
    {
        if (_target != null)
        {
            return Vector3.Distance(_target.position, transform.position);
        }

        return 0;
    }

    private void Reset()
    {
        _target = null;
        _movementTimer = 0;
    }
}
