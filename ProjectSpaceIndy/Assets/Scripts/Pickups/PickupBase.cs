using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    public float LifeTime;
    public float AttractionRadius;
    public float AttractionSpeed;
    private Transform _target;
    private float _movementTimer;
    private bool _permanent;
    private float _lifeTimeTimer;

    protected virtual void Awake()
    {
        if (LifeTime == 0) _permanent = true;
        else _permanent = false;
        _lifeTimeTimer = 0;
    }
    
    protected virtual void Update()
    {
        if (!_permanent)
        {
            if (_lifeTimeTimer >= LifeTime)
            {
                Destroy(gameObject);
            }
            _lifeTimeTimer += TimerManager.Instance.GameDeltaTime;
        }
        
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
            _movementTimer += TimerManager.Instance.GameDeltaTime * AttractionSpeed;
            transform.position = Vector3.Lerp(transform.position, _target.position, _movementTimer);
        }

        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        if (player != null)
        {
            GrantEffect(player);
            Destroy(gameObject);
        }
    }

    protected virtual float DistanceToTarget()
    {
        if (_target != null)
        {
            return Vector3.Distance(_target.position, transform.position);
        }

        return 0;
    }

    protected virtual void ResetPickup()
    {
        _target = null;
        _movementTimer = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttractionRadius);
    }

    protected abstract void GrantEffect(PlayerUnit playerUnit);
}
