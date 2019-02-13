using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    public float LifeTime;
    private bool _permanent;
    private float _lifeTimeTimer;

    protected virtual void Awake()
    {
        if (LifeTime == 0) _permanent = true;
        else _permanent = false;
        _lifeTimeTimer = 0;
    }

    protected virtual void Update()
    {
        if (!_permanent)
        {
            if (_lifeTimeTimer >= LifeTime)
            {
                Destroy(gameObject);
            }
            _lifeTimeTimer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        if (player != null)
        {
            GrantEffect(player);
            Destroy(gameObject);
        }
    }

    protected abstract void GrantEffect(PlayerUnit playerUnit);
}
