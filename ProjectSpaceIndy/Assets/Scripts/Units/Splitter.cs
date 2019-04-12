using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    public Animator Animator;
    private float _baseSpeed;

    private void Awake()
    {
        _baseSpeed = Animator.speed;
    }

    private void Update()
    {
        Animator.speed = _baseSpeed * TimerManager.Instance.GameDeltaScale;
    }
}
