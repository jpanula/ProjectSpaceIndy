using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamageReceiver
{

    public Weapon[] Weapons;
    public ParticleSystem DeathEffect;
    
    public IMover Mover;
    public IHealth Health;
    private PooledSpawner _spawner;
    protected bool _dead;

    public PooledSpawner Spawner { get; set; }

    public virtual void Awake()
    {
        Health = gameObject.GetOrAddComponent<Health>();
        Mover = GetComponent<IMover>();
    }

    protected abstract void Update();

    public virtual bool TakeDamage(int amount)
    {
        bool died = Health.DecreaseHealth(amount);
        if (died && !_dead)
        {
            _dead = true;
            Die();
        }

        return died;
    }

    public void Fire()
    {
        foreach (Weapon weapon in Weapons)
        {
            weapon.Fire();
        }
    }

    protected virtual void Die()
    {
        if (DeathEffect != null)
        {
            DeathEffect.Play();
        }
        if (Spawner != null)
        {
            ResetUnit();
            Spawner.ReturnUnit(this);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    protected virtual void ResetUnit()
    {
        Health.ResetHealth();
        Mover.ResetMover();
        _dead = false;
    }
}
