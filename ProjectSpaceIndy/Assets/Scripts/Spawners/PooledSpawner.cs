using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class PooledSpawner : Spawner<UnitBase>
{
    public UnitPool Pool;

    public override UnitBase Spawn()
    {
        UnitBase result = null;
        PreSpawn();
        result = Pool.GetPooledItem();
        result.Spawner = this;
        PostSpawn(result);
        return result;
    }

    public bool ReturnUnit(UnitBase unit)
    {
        return Pool.ReturnPooledItem(unit);
    }
}
