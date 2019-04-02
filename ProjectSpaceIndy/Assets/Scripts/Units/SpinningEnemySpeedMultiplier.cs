using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningEnemySpeedMultiplier : MonoBehaviour
{
    public float SpeedMultiplier;
    public Animator animator;
    public Weapon[] Weapons;
    
    private PartDestroyer _partDestroyer;
    private GameObject[] _parts;
    private int _numberOfParts;
    private float _baseAnimatorSpeed;
    private float[] _weaponCooldowns;
    private float _scaledAnimatorSpeed;

    private void Awake()
    {
        _partDestroyer = GetComponent<PartDestroyer>();
    }

    private void Start()
    {
        _parts = _partDestroyer.GameObjects;
        _baseAnimatorSpeed = animator.speed;
        _weaponCooldowns = new float[Weapons.Length];
        
        for (int i = 0; i < Weapons.Length; i++)
        {
            _weaponCooldowns[i] = Weapons[i].CoolDownTime;
        }
    }

    private void LateUpdate()
    {
        _scaledAnimatorSpeed = _baseAnimatorSpeed * TimerManager.Instance.GameDeltaScale;
        float speedModifier = 1;
        foreach (var part in _parts)
        {
            if (part.activeSelf == false || part == null)
            {
                speedModifier *= SpeedMultiplier;
            }
        }

        for (int i = 0; i < Weapons.Length; i++)
        {
            var weapon = Weapons[i];
            if (weapon.gameObject.activeSelf || weapon.gameObject != null)
            {
                weapon.CoolDownTime = _weaponCooldowns[i] / speedModifier;
            }
        }
        animator.speed = speedModifier * _scaledAnimatorSpeed;
    }
}
