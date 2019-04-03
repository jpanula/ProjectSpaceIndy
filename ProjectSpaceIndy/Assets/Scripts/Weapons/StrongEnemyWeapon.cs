using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemyWeapon : Weapon
{
    public int ShotsBetweenBursts;
    public int ProjectilesInBurst;
    public float BurstAngle;
    public float BurstLength;
    private int _shots;
    private int _burstShots;
    private float _angleBetweenProjectiles;
    private float _timeBetweenProjectiles;
    private float _burstTimer = float.PositiveInfinity;
    private Vector3 _startAngle;
    private bool _bursting;
    public override bool Fire()
    {
        if (_shots >= ShotsBetweenBursts && (_coolDownTimer >= CoolDownTime || _bursting))
        {
            return FireBurst();
        }
        if (_shots < ShotsBetweenBursts)
        {

            _burstTimer = float.PositiveInfinity;
            _burstShots = 0;
            if (base.Fire())
            {
                _shots++;
                return true;
            }
        }

        return false;
    }

    public bool FireBurst()
    {
        _bursting = true;
        _angleBetweenProjectiles = BurstAngle / (ProjectilesInBurst - 1);
        _timeBetweenProjectiles = BurstLength / (ProjectilesInBurst - 1);
        _startAngle = Quaternion.Euler(0, -BurstAngle / 2, 0) * transform.forward;
        
        if (_burstShots >= ProjectilesInBurst)
        {
            _shots = 0;
            _coolDownTimer = 0;
            _bursting = false;
            return false;
        }

        _burstTimer += TimerManager.Instance.GameDeltaTime;
        if (_burstTimer >= _timeBetweenProjectiles)
        {
            _burstTimer = 0;
            Projectile projectile = Projectiles.GetPooledItem();
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                Vector3 newDirection = Quaternion.Euler(0, _burstShots * _angleBetweenProjectiles, 0) * _startAngle;
                projectile.transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
                projectile.Launch(this, newDirection);
                if (AudioSource != null)
                {
                    AudioSource.Play();
                }
                _burstShots++;
                return true;
            }
        }

        return false;
    }
}
