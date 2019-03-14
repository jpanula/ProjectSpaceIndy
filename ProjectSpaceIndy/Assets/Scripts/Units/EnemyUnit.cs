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

    protected override void Die()
    {
        List<PickupBase> scraps = new List<PickupBase>();
        for (int i = 0; i < AmountOfScrapToDrop; i++)
        {
            scraps.Add(PickupManager.Instance.GetScrap());
        }
        Vector3 dropAngle = Vector3.forward * DropDistance;
        for (int i = 0; i < scraps.Count; i++)
        {
            scraps[i].transform.position = dropAngle + transform.position;
            dropAngle = Quaternion.AngleAxis(360.0f / scraps.Count, Vector3.up) * dropAngle;
        }
        base.Die();
    }

    protected override void ResetUnit()
    {
        base.ResetUnit();
        _timer = 0;
    }
}
