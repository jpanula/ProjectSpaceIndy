using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<TMonoBehaviour> : MonoBehaviour
    where TMonoBehaviour : MonoBehaviour
{
    /// <summary>
    /// Prefab to be instantiated by the spawner
    /// </summary>
    public TMonoBehaviour prefab;

    /// <summary>
    /// Sets the prefab to instantiate
    /// </summary>
    /// <param name="prefab"></param>
    public void SetPrefab(TMonoBehaviour prefab)
    {
        this.prefab = prefab;
    }

    /// <summary>
    /// Instantiates the prefab to be spawned
    /// </summary>
    /// <returns>The copy created from the prefab</returns>
    public TMonoBehaviour Spawn()
    {
        TMonoBehaviour result = null;

        PreSpawn();
        result = Instantiate(prefab, transform.position, transform.rotation);
        
        PostSpawn(result);
        return result;
    }

    /// <summary>
    /// Actions to perform before spawning
    /// </summary>
    protected virtual void PreSpawn()
    {
    }

    /// <summary>
    /// Actions to perform after spawning
    /// </summary>
    /// <param name="spawnedObject">The copy created from the prefab</param>
    protected virtual void PostSpawn(TMonoBehaviour spawnedObject)
    {
    }
}
