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
    [SerializeField, Tooltip("How long before spawn does the spawner warn about enemies")]
    private float _warningTime;
    private float _warningTimer;
    private bool _warningStarted;
    [Tooltip("Delay before warning (and spawn) after activator has activated")]
    public float WarningDelay;

    public GameObject WarningSprite;

    public bool FinishedSpawning
    {
        get { return _finishedSpawning; }
    }

    private void Awake()
    {
        if (MaxSpawns == 0) _endlessSpawn = true;
        _ActivatorExists = Activator != null;
        _finishedSpawning = false;
        _warningStarted = false;
    }

    private void Start()
    {
        _maxSimultaneousSpawns = Pool.Size;
    }

    private void Update()
    {
        if (Activator.Active && WarningSprite != null && _finishedSpawning == false || _warningStarted)
        {
            _warningStarted = true;
            _warningTimer += Time.deltaTime * TimerManager.Instance.GameDeltaScale;
            if(_warningTimer >= WarningDelay && !WarningSprite.activeSelf)
            {
                WarningSprite.SetActive(true);
            }
        }
        
        
        _spawnTimer += TimerManager.Instance.GameDeltaTime;
        if (_currentSpawns < _maxSimultaneousSpawns && (MaxSpawns > 0 || _endlessSpawn) && _spawnTimer > SpawnCooldown && (!_ActivatorExists || Activator.Active) && _warningTimer >= _warningTime)
        {
            _warningStarted = false;
            if(WarningSprite != null)
            {
                WarningSprite.SetActive(false);
            }
            UnitBase enemy = Spawn();
            enemy.transform.position = transform.position;
            _spawnTimer = 0;
            _warningTimer = 0;
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
