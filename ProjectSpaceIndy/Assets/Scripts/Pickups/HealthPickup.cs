using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupBase
{
    [Tooltip("Amount of health to grant")]
    public int HealthAmount;
    
    protected override void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        if (player != null)
        {
            GrantEffect(player);
            ResetPickup();
            if (!PickupManager.Instance.ReturnHealth(this))
            {
                Destroy(gameObject);
            }
        }
    }

    protected override void GrantEffect(PlayerUnit playerUnit)
    {
        playerUnit.GetComponent<Health>().IncreaseHealth(HealthAmount);
    }
}
