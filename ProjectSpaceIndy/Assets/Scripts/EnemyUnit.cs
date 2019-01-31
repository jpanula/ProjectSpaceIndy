using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    public float CoolDownTime;
    private float _timer;
    
    protected override void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= CoolDownTime)
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.Fire();
            }

            _timer = 0;
        }
    }
}
