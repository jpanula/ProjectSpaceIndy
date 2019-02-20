using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickup : PickupBase
{
    [Tooltip("Amount of fuel to grant")]
    public float Amount;
    protected override void GrantEffect(PlayerUnit playerUnit)
    {
        playerUnit.FuelAmount += Amount;
    }

    /*protected override void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        if (player != null)
        {
            GrantEffect(player);
            PickupManager.Instance.ReturnFuel(this);
        }
    }*/
}
