using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class PooledSpawner : MonoBehaviour
{
    public UnitPool Pool;

    public UnitBase Spawn()
    {
        UnitBase result = null;
        PreSpawn();
        result = Pool.GetPooledItem();
        result.Spawner = this;
        PostSpawn(result);
        return result;
    }

    public virtual bool ReturnUnit(UnitBase unit)
    {
        return Pool.ReturnPooledItem(unit);
    }

    protected virtual void PreSpawn()
    {
    }

    protected virtual void PostSpawn(UnitBase spawnedUnit)
    {
    }
    
}
