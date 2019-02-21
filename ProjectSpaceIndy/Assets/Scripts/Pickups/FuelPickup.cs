using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FuelPickup : PickupBase
{
    [Tooltip("Amount of fuel to grant")]
    public float FuelAmount;
    protected override void GrantEffect(PlayerUnit playerUnit)
    {
        playerUnit.FuelAmount += FuelAmount;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        if (player != null)
        {
            GrantEffect(player);
            Reset();
            if (!PickupManager.Instance.ReturnFuel(this))
            {
                Destroy(gameObject);
            }
        }
    }
}
