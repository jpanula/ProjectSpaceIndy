using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
