using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamageReceiver
{

    public IMover Mover;
    public IHealth Health;
    public Weapon[] Weapons;
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
        if (Spawner != null)
        {
            Reset();
            Spawner.ReturnUnit(this);
        }
        else
        {
            Destroy(gameObject);
        }

        _dead = true;
    }

    protected virtual void Reset()
    {
        Health.Reset();
        Mover.Reset();
        _dead = false;
    }
}
