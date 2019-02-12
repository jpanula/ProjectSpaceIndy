using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPool : GenericPool<PickupBase>
{
        
    protected override void Activate(PickupBase item)
    {
        base.Activate(item);
        item.gameObject.SetActive(true);
    }

    protected override void Deactivate(PickupBase item)
    {
        base.Deactivate(item);
        item.gameObject.SetActive(false);
    }
}
