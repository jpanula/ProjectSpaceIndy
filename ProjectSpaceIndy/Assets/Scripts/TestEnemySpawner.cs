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
    }
}
