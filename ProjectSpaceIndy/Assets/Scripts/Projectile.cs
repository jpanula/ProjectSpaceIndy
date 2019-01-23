﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Lifetime;
    public int Damage;
    public IMover Mover;
    private Weapon _weapon;
    private Vector3 _direction;
    private bool _isFired;
    private float _lifeTimeTimer;

    private void Awake()
    {
        Mover = GetComponent<IMover>();
        _lifeTimeTimer = 0;
    }

    private void Update()
    {
        _lifeTimeTimer += Time.deltaTime;
        if (_lifeTimeTimer >= Lifetime)
        {
            ReturnProjectile();
        }

        if (_isFired)
        {
            Mover.Move(_direction * Time.deltaTime);
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

    private void OnTriggerEnter(Collider other)
    {
        IDamageReceiver damageReceiver = other.GetComponent<IDamageReceiver>();
        if (damageReceiver != null)
        {
            damageReceiver.TakeDamage(Damage);
            ReturnProjectile();
        }
    }
}
