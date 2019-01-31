using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySpawner : Spawner<EnemyUnit>
{
    private void Update()
    {
        if (!GameObject.FindWithTag("Enemy"))
        {
            Spawn();
        }
        // Key to spawn more enemies for testing purposes
        if (Input.GetKeyDown(KeyCode.P))
        {
            Spawn();
        }
    }
}
