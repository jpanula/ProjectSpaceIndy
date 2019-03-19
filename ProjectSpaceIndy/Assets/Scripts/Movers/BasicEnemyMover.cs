using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMover : MonoBehaviour, IMover
{
    
    public float TurnSpeed;
    public float _speed;
    public ForwardVector Forward = ForwardVector.Forward;
    private LayerMask _layerMask = (int) (Const.Layers.Player | Const.Layers.Activator | Const.Layers.Environment | Const.Layers.Enemy);
    private SphereCollider _collider;

    public enum ForwardVector
    {
        Forward,
        MovementVector
    }
    
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public Vector3 MovementVector { get; set; }
    
    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (MovementVector != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(MovementVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed);
        }

        var t = transform;
        var position = t.position;
        Vector3 newPos;
        switch (Forward)
        {
            case ForwardVector.Forward:
                newPos = position + t.forward * Speed;
                break;
            case ForwardVector.MovementVector:
                newPos = position + MovementVector.normalized * Speed;
                break;
            default:
                newPos = position + t.forward * Speed;
                break;
        }
        
        newPos = Vector3.Lerp(position, newPos, Time.deltaTime);
        Vector3 direction = newPos - position;

        RaycastHit hit;
        if (Physics.SphereCast(position, _collider.radius, direction, out hit,
            Vector3.Distance(position, newPos), _layerMask))
        {
            direction = Vector3.ProjectOnPlane(direction, hit.normal);
            newPos = position + direction;

            if (Physics.SphereCast(position, _collider.radius, direction, out hit,
                Vector3.Distance(position, newPos), _layerMask))
            {
                direction = Vector3.ProjectOnPlane(direction, hit.normal);
                newPos = position + direction;
            }
        }
        
        transform.position = newPos;
    }
    
    public void ResetMover()
    {
        Speed = 0;
        MovementVector = Vector3.zero;
    }
}
