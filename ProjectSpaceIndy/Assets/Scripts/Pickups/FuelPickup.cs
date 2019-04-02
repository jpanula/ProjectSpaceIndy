using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FuelPickup : PickupBase
{
    [Tooltip("Amount of fuel to grant")]
    public float FuelAmount;

    private Animator _animator;
    private float _animatorSpeed;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponentInChildren<Animator>();
        _animatorSpeed = _animator.speed;
    }

    protected override void GrantEffect(PlayerUnit playerUnit)
    {
        playerUnit.FuelAmount += FuelAmount;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        if (player != null)
        {
            GrantEffect(player);
            ResetPickup();
            if (!PickupManager.Instance.ReturnFuel(this))
            {
                Destroy(gameObject);
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        _animator.speed = _animatorSpeed * TimerManager.Instance.GameDeltaScale;
    }
}
