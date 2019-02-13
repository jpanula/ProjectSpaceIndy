using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Lifetime;
    public int Damage;
    public LayerMask LayerMask;
    public IMover Mover;
    public float Speed;
    private Weapon _weapon;
    private Vector3 _direction;
    public bool _isFired;
    private float _lifeTimeTimer;

    private void Awake()
    {
        Mover = GetComponent<IMover>();
        Mover.Speed = Speed;
        _lifeTimeTimer = 0;
    }

    private void Update()
    {
        if (_isFired)
        {
            Mover.MovementVector = _direction * Time.deltaTime;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Mover.MovementVector, out hit,
                Vector3.Distance(transform.position, transform.position + Mover.MovementVector * Speed), LayerMask))
            {
                Hit(hit.collider);
            }
        }
        else
        {
            Mover.MovementVector = Vector3.zero;
        }
        
        _lifeTimeTimer += Time.deltaTime;
        if (_lifeTimeTimer >= Lifetime)
        {
            ReturnProjectile();
        }
    }

    public void Launch(Weapon weapon, Vector3 direction)
    {
        _weapon = weapon;
        _direction = direction;
        _isFired = true;
        _lifeTimeTimer = 0;
    }

    public void ReturnProjectile()
    {
        Weapon owner = _weapon;
        _weapon = null;
        _isFired = false;
        _lifeTimeTimer = 0;
        owner.ReturnProjectile(this);
    }

    private void Hit(Collider collider)
    {
        IDamageReceiver damageReceiver = collider.GetComponent<IDamageReceiver>();
        if (damageReceiver != null)
        {
            damageReceiver.TakeDamage(Damage);
            ReturnProjectile();
        }
        else if (1 << collider.gameObject.layer == (int) Const.Layers.Environment || 1 << collider.gameObject.layer == (int) Const.Layers.Activator)
        {
            ReturnProjectile();
        }
    }
}
