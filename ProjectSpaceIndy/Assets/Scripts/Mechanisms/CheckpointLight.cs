using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLight : MonoBehaviour
{
    public ActivatorBase Activator;
    private Material _defaultColour;
    public Material ActivatedColour;
    public float Speed;
    private Renderer _renderer;
    private bool _active;
    private float _timer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColour = _renderer.material;
        _timer = 0;
        _active = false;
    }

    void Update()
    {
        if (Activator.Active && !_active)
        {
            _timer += (Time.deltaTime * TimerManager.Instance.GameDeltaScale) * Speed;
            _renderer.material.Lerp(_defaultColour, ActivatedColour, _timer);
            if (_timer >= 1)
            {
                _active = true;
            }
        }
    }
}
