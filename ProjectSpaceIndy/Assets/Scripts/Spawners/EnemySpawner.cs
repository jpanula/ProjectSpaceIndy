using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : PooledSpawner
{
    
    
    [Tooltip("If 0, will spawn without end")]
    public int MaxSpawns;

    [Tooltip("If no activator, the spawner will start spawning instantly")]
    public ActivatorBase Activator;
    public float SpawnCooldown;
    
    private int _maxSimultaneousSpawns;
    private int _currentSpawns;
    private float _spawnTimer;
    private bool _endlessSpawn;
    private bool _ActivatorExists;

    private bool _finishedSpawning;

    public bool FinishedSpawning
    {
        get { return _finishedSpawning; }
    }

    private void Awake()
    {
        if (MaxSpawns == 0) _endlessSpawn = true;
        _ActivatorExists = Activator != null;
        _finishedSpawning = false;
    }

    private void Start()
    {
        _maxSimultaneousSpawns = Pool.Size;
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_currentSpawns < _maxSimultaneousSpawns && (MaxSpawns > 0 || _endlessSpawn) && _spawnTimer > SpawnCooldown && (!_ActivatorExists || Activator.Active))
        {
            UnitBase enemy = Spawn();
            enemy.transform.position = transform.position;
            _spawnTimer = 0;
            _currentSpawns++;
            if (!_endlessSpawn) MaxSpawns--;
        }

        if (!_finishedSpawning && (MaxSpawns == 0 && !_endlessSpawn))
        {
            _finishedSpawning = true;
        }
    }

    public override bool ReturnUnit(UnitBase unit)
    {
        _currentSpawns--;
        return base.ReturnUnit(unit);
    }
}
