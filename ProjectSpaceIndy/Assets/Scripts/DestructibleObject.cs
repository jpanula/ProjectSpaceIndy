﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamageReceiver
{
    private Health _health;
    public int AmountOfScrapToDrop;
    public float DropDistance;
    [Tooltip("Max distance from player for object to be destroyed")]
    public float MaxDistance = 22f;
    

    private void Awake()
    {
        _health = GetComponent<Health>();
        if (MaxDistance < 1)
        {
            MaxDistance = 22;
        }
    }

    public virtual bool TakeDamage(int amount)
    {
        bool destroyed = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(player.transform.position, transform.position) <= MaxDistance)
        {

            destroyed = _health.DecreaseHealth(amount);
            if (destroyed)
            {
                Die();
            }
        }

        return destroyed;
    }

    protected virtual void Die()
    {
        List<PickupBase> scraps = new List<PickupBase>();
        for (int i = 0; i < AmountOfScrapToDrop; i++)
        {
            scraps.Add(PickupManager.Instance.GetScrap());
        }
        Vector3 dropAngle = Vector3.forward * DropDistance;
        for (int i = 0; i < scraps.Count; i++)
        {
            scraps[i].transform.position = dropAngle + transform.position;
            dropAngle = Quaternion.AngleAxis(360.0f / scraps.Count, Vector3.up) * dropAngle;
        }
        
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, MaxDistance);
    }
}
