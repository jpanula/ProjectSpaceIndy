using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    public Animator Animator;
    private float _baseSpeed;
    [SerializeField, Tooltip("How long the Splitter is invulnerable after spawn")]
    private float _invulnerabilityTime;
    private float _timer;
    private bool _timerIsDone;

    private void Awake()
    {
        _baseSpeed = Animator.speed;
        _timer = 0;
        _timerIsDone = false;
        GetComponent<Health>()._isInvulnerable = true;
    }

    private void Update()
    {
        Animator.speed = _baseSpeed * TimerManager.Instance.GameDeltaScale;

        if (_timer >= _invulnerabilityTime && !_timerIsDone)
        {
            GetComponent<Health>()._isInvulnerable = false;
            _timerIsDone = true;
        }

        if (!_timerIsDone)
        {
            _timer += Time.deltaTime * TimerManager.Instance.GameDeltaScale;
        }
    }
}
