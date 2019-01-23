using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float CoolDownTime;
    public ProjectilePool Projectiles;
    private float _coolDownTimer;

    private void Awake()
    {
        _coolDownTimer = 0;
    }

    private void Update()
    {
        _coolDownTimer += Time.deltaTime;
    }
    
    /// <summary>
    /// Fires a projectile
    /// </summary>
    /// <returns>True if the projectile can be fired, false otherwise</returns>
    public bool Fire()
    {
        bool canFire = _coolDownTimer >= CoolDownTime;
        if (canFire)
        {
            _coolDownTimer = 0;
            Projectile projectile = Projectiles.GetPooledItem();
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            projectile.Launch(this, transform.forward);
        }

        return canFire;
    }

    public bool ReturnProjectile(Projectile projectile)
    {
        return Projectiles.ReturnPooledItem(projectile);
    }

}
