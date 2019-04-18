using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    public float CoolDownTime;
    public int AmountOfScrapToDrop;
    public float DropDistance;
    private float _timer;
    
    protected override void Update()
    {
        _timer += TimerManager.Instance.GameDeltaTime;

        if (_timer >= CoolDownTime)
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.Fire();
            }

            _timer = 0;
        }
    }

    protected override void ResetUnit()
    {
        base.ResetUnit();
        _timer = 0;
    }
}
