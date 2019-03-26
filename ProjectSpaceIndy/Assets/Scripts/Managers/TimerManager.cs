using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private float _gameDeltaTime = 0;
    private float _uiDeltaTime = 0;
    [SerializeField, Range(0,1)]
    private float _gameDeltaScale = 1;
    [SerializeField, Range(0,1)]
    private float _uiDeltaScale = 1;
    private float _gameTime = 0;
    private float _scaledGameTime;

    public float ScaledGameTime
    {
        get { return _scaledGameTime; }
    }

    public float GameTime
    {
        get { return _gameTime; }
    }

    public float GameDeltaTime
    {
        get { return _gameDeltaTime; }
    }

    public float UiDeltaTime
    {
        get { return _uiDeltaTime; }
    }

    public float GameDeltaScale
    {
        get { return _gameDeltaScale; }
    }

    public float UiDeltaScale
    {
        get { return _uiDeltaScale; }
    }

    public void SetGameTimeScale(float timeScale)
    {
        _gameDeltaScale = Mathf.Clamp01(timeScale);
    }

    public void SetUITimeScale(float timeScale)
    {
        _uiDeltaScale = Mathf.Clamp01(timeScale);
    }

    private void Update()
    {
        _gameDeltaTime = Time.deltaTime * _gameDeltaScale;
        _uiDeltaTime = Time.deltaTime * _uiDeltaScale;
        _gameTime += Time.deltaTime;
        _scaledGameTime += _gameDeltaTime;
    }
}
