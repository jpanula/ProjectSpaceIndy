using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float CoolDownTime;
    public ProjectilePool Projectiles;
    private float _coolDownTimer;

    public AudioSource AudioSource;

    private void Awake()
    {
        _coolDownTimer = 0;
    }

    private void Update()
    {
        _coolDownTimer += TimerManager.Instance.GameDeltaTime;
    }
    
    /// <summary>
    /// Fires a projectile
    /// </summary>
    /// <returns>True if the projectile can be fired, false otherwise</returns>
    public bool Fire()
    {
        if (_coolDownTimer >= CoolDownTime)
        {
            _coolDownTimer = 0;
            Projectile projectile = Projectiles.GetPooledItem();
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.Launch(this, transform.forward);
                if (AudioSource != null)
                {
                    AudioSource.Play();
                }
                
                return true;
            }
        }

        return false;
    }

    public bool ReturnProjectile(Projectile projectile)
    {
        return Projectiles.ReturnPooledItem(projectile);
    }

}
