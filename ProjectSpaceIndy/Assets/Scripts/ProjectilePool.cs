using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : GenericPool<Projectile> {
    
    protected override void Activate(Projectile item)
    {
        base.Activate(item);
        item.gameObject.SetActive(true);
    }

    protected override void Deactivate(Projectile item)
    {
        base.Deactivate(item);
        item.gameObject.SetActive(false);
    }
}
