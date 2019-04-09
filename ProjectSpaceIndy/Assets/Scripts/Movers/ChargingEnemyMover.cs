using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemyMover : MonoBehaviour, IMover
{
    public float _speed;
    public LayerMask CollideWith;
    public SphereCollider _collider;
    public float Speed
    {
        get { return _speed;}
        set { _speed = value; }
    }
    public Vector3 MovementVector { get; set; }

    private void Awake()
    {
        if (_collider == null)
        {
            _collider = GetComponent<SphereCollider>();
        }
    }

    public void ResetMover()
    {
        
    }

    public void Move()
    {
        var pos = transform.position;
        Vector3 newPos = pos + MovementVector.normalized * Speed;
        newPos = Vector3.Lerp(pos, newPos, TimerManager.Instance.GameDeltaTime);
        Vector3 direction = newPos - pos;
        
        RaycastHit hit;
        while (Physics.SphereCast(pos, _collider.radius, direction, out hit,
            Vector3.Distance(pos, newPos), CollideWith))
        {
            direction = Vector3.ProjectOnPlane(direction, hit.normal);
            newPos = pos + direction;

            if (direction.magnitude < 0.00001)
            {
                newPos = transform.position;
                break;
            }
        }
        transform.position = newPos;
    }

    private void Update()
    {
        Move();
    }
}
