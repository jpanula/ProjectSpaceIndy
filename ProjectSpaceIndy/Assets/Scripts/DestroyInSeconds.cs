using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    public float TimeBeforeDestroy;
    private float _timer;
    private bool _startCountDown;

    private void Awake()
    {
        _startCountDown = true;
    }

    void Update()
    {
        if (_startCountDown)
        {
            _timer += TimerManager.Instance.GameDeltaTime;

            if (_timer >= TimeBeforeDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
