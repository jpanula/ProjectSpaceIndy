using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueRotation : MonoBehaviour
{
    public float Speed;
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        var color = _material.color;
        float[] newColor = new float[4];
        Color.RGBToHSV(color, out newColor[0], out newColor[1], out newColor[2]);
        newColor[0] += TimerManager.Instance.GameDeltaTime * Speed;
        color = Color.HSVToRGB(newColor[0], newColor[1], newColor[2]);
        color.a = _material.color.a;
        _material.color = color;
    }
}
