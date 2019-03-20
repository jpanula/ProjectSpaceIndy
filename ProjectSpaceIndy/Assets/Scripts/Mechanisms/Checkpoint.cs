using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool _activated;
    public ActivatorBase Activator;
    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Activator.Active && !_activated)
        {
            _player.GetComponent<PlayerUnit>().CheckpointPos = transform.position;
            _player.GetComponent<PlayerUnit>().PassedCheckpoint = true;
            _activated = true;
        }
    }
}
