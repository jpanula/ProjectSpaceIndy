using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : PooledSpawner
{
    
    
    [Tooltip("If 0, will spawn without end")]
    public int MaxSpawns;

    public ActivatorBase Activator;
    public float SpawnCooldown;
    
    private int _maxSimultaneousSpawns;
    private int _currentSpawns;
    private float _spawnTimer;
    
    private void Start()
    {
        _maxSimultaneousSpawns = Pool.Size;
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (Activator.Active)
        {
            if (_currentSpawns < _maxSimultaneousSpawns && MaxSpawns > 0 && _spawnTimer > SpawnCooldown)
            {
                UnitBase enemy = Spawn();
                enemy.transform.position = transform.position;
                _spawnTimer = 0;
                _currentSpawns++;
                MaxSpawns--;
            }
        }
    }

    public override bool ReturnUnit(UnitBase unit)
    {
        _currentSpawns--;
        return base.ReturnUnit(unit);
    }
}
