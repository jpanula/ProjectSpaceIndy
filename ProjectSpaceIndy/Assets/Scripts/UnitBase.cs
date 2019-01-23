using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamageReceiver
{

    public IMover Mover;
    public IHealth Health;
    public Weapon[] Weapons;

    protected virtual void Awake()
    {
        Health = gameObject.GetOrAddComponent<Health>();
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
        Destroy(gameObject);
    }
}
