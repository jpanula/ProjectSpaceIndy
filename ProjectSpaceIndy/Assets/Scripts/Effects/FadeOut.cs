using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float FadeTime;
    public float StartAlpha;
    private Material _material;
    private float _newAlpha;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b,StartAlpha);
        _newAlpha = 1;
    }

    private void Update()
    {
        if (_newAlpha < 0)
        {
            Destroy(gameObject);
        }
        _newAlpha -= TimerManager.Instance.GameDeltaTime / FadeTime;
        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b,_newAlpha * StartAlpha);
    }
}
