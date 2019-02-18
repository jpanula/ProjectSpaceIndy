using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamageReceiver
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public virtual bool TakeDamage(int amount)
    {
        bool destroyed = _health.DecreaseHealth(amount);
        if (destroyed)
        {
            Die();
        }

        return destroyed;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
