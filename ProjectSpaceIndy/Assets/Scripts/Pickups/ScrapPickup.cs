using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPickup : PickupBase
{
    public int Score;
    
    protected override void GrantEffect(PlayerUnit playerUnit)
    {
        GameManager.Score += Score;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        if (player != null)
        {
            if (PickupSound != null)
            {
                Instantiate(PickupSound);
                PickupSound.transform.position = transform.position;
            }
            GrantEffect(player);
            ResetPickup();
            if (!PickupManager.Instance.ReturnScrap(this))
            {
                Destroy(gameObject);
            }
        }
    }
}
