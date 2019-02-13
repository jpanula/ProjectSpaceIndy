using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamageReceiver
{

    public IMover Mover;
    public IHealth Health;
    public Weapon[] Weapons;
    private PooledSpawner _spawner;

    public PooledSpawner Spawner { get; set; }

    protected virtual void Awake()
    {
        Health = gameObject.GetOrAddComponent<Health>();
        Mover = GetComponent<IMover>();
    }

    protected abstract void Update();

    public virtual bool TakeDamage(int amount)
    {
        bool died = Health.DecreaseHealth(amount);
        if (died)
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
            Spawner.ReturnUnit(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
