using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWallInvisRoom : DestructibleObject
{
    public GameObject[] ObjectsToEnable;

    protected override void Die()
    {
        foreach (var gameObject in ObjectsToEnable)
        {
            if (gameObject.activeSelf == false)
            {
                gameObject.SetActive(true);
            }
        }
        base.Die();
    }
}
