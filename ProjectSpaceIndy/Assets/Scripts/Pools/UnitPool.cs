using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPool : GenericPool<UnitBase>
{
    protected override void Activate(UnitBase item)
    {
        base.Activate(item);
        item.gameObject.SetActive(true);
    }

    protected override void Deactivate(UnitBase item)
    {
        base.Deactivate(item);
        item.gameObject.SetActive(false);
    }
}
