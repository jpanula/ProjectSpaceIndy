using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActivator : ActivatorBase
{
    private bool _active;
    public EnemySpawner[] ActivatorSpawners;
    public ActivatorBase[] Activators;

    public override bool Active
    {
        get { return _active; }
        set { _active = value; }
    }

    private void Update()
    {
        int activeCount = 0;

        foreach (EnemySpawner spawner in ActivatorSpawners)
        {
            if (spawner.FinishedSpawning)
            {
                activeCount++;
            }
        }

        foreach (ActivatorBase activator in Activators)
        {
            if (activator.Active)
            {
                activeCount++;
            }
        }

        if (activeCount == (ActivatorSpawners.Length + Activators.Length))
        {
            _active = true;
        }

        if (activeCount < (ActivatorSpawners.Length + Activators.Length))
        {
            _active = false;
        }
    }
}
