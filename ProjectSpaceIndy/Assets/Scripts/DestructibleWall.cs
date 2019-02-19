using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : DestructibleObject
{
    public GameObject Ceiling;

    protected override void Die()
    {
        Destroy(Ceiling);
        base.Die();
    }
}
