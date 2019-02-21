﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Lifetime;
    public int Damage;
    public LayerMask LayerMask;
    public IMover Mover;
    public float HitBoxRadius;
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

    private void FixedUpdate()
    {
        if (_isFired)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, HitBoxRadius, Mover.MovementVector, out hit,
                Vector3.Distance(transform.position, transform.position + Mover.MovementVector * Speed * Time.deltaTime), LayerMask))
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
        Mover.MovementVector = _direction;
        _lifeTimeTimer = 0;
    }

    public void ReturnProjectile()
    {
        Weapon owner = _weapon;
        Reset();
        owner.ReturnProjectile(this);
    }

    private void Hit(Collider hitCollider)
    {
        IDamageReceiver damageReceiver = hitCollider.GetComponent<IDamageReceiver>();
        if (damageReceiver != null)
        {
            damageReceiver.TakeDamage(Damage);
            ReturnProjectile();
        }
        else if (1 << hitCollider.gameObject.layer == (int) Const.Layers.Environment || 1 << hitCollider.gameObject.layer == (int) Const.Layers.Activator)
        {
            ActivatorBase activator = hitCollider.GetComponent<ActivatorBase>();
            if (activator != null)
            {
                activator.Active = true;
            }
            ReturnProjectile();
        }
    }

    private void Reset()
    {
        _weapon = null;
        _isFired = false;
        _lifeTimeTimer = 0;
        Mover.MovementVector = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, HitBoxRadius);
    }
}
