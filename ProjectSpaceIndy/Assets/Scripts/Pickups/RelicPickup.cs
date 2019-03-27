using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicPickup : PickupBase
{
    public int Score;
    public bool FinalRelic;


    protected override void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        if (player != null)
        {
            GrantEffect(player);
            if (FinalRelic)
            {
                GameManager.EscapePhase = true;
            }
            Destroy(gameObject);
        }
    }

    protected override void GrantEffect(PlayerUnit playerUnit)
    {
        GameManager.Score += Score;
    }
}
