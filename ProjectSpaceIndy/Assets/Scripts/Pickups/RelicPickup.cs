using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicPickup : PickupBase
{
    public int Score;
    public bool FinalRelic;


    protected override void Awake()
    {
        GameManager.TotalRelics = GameManager.TotalRelics + 1;
        base.Awake();
    }

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

            GameManager.RelicsCollected = GameManager.RelicsCollected + 1;
            Destroy(gameObject);
        }
    }

    protected override void GrantEffect(PlayerUnit playerUnit)
    {
        GameManager.Score += Score;
    }
}
