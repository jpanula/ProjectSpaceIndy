﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObstacle : MonoBehaviour
{
    public ActivatorBase[] Activators;
    [Tooltip("Gameobjects that will deactivate when the lasers turn off")]
    public GameObject[] ObjectsToDeactivate;

    public AudioSource DeactivationSound;

    private BoxCollider _boxCollider;
    private bool _hasPlayedOnce;
    private bool _isAudioNull;
    private float _volume;

    private void Start()
    {
        _boxCollider = this.GetComponent<BoxCollider>();
        _hasPlayedOnce = false;
        _isAudioNull = DeactivationSound == null;
        if (!_isAudioNull)
        {
            _volume = DeactivationSound.volume;
        }
    }

    void Update()
    {
        if(Activators.Length > 0)
        {
            int counter = 0;

            for (int i = 0; i < Activators.Length; i++)
            {
                if (Activators[i].Active)
                {
                    counter++;
                }
            }

            if (counter == Activators.Length)
            {
                if (_isAudioNull && !_hasPlayedOnce)
                {
                    DeactivationSound.volume = _volume * AudioManager.EffectsVolume;
                    DeactivationSound.Play();
                    _hasPlayedOnce = true;
                }
                DeactivateLasers();
            }
        }
    }

    private void DeactivateLasers()
    {
        foreach (GameObject gameObject in ObjectsToDeactivate)
        {
            gameObject.SetActive(false);
        }
        Destroy(_boxCollider);
    }
}
