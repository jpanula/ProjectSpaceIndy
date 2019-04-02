using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDeltaScaler : MonoBehaviour
{
    public Animator Animator;
    private float _animatorSpeed;

    private void Awake()
    {
        _animatorSpeed = Animator.speed;
    }

    private void Update()
    {
        Animator.speed = _animatorSpeed * TimerManager.Instance.GameDeltaScale;
    }
}
