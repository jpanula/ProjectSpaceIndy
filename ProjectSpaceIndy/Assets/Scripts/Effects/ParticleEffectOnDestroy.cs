﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleEffectOnDestroy : MonoBehaviour
{
    public ParticleSystem Effect;
    private bool _isQuitting;

    private void OnEnable()
    {
        ResultScreen.SceneChanging += OnSceneChanging;
    }

    private void OnDisable()
    {
        ResultScreen.SceneChanging -= OnSceneChanging;
    }

    private void OnSceneChanging()
    {
        _isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!_isQuitting)
        {
            GameObject effectObject = Instantiate(Effect.gameObject, transform.position, transform.rotation);
            var main = effectObject.GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Destroy;
        }
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }
}
