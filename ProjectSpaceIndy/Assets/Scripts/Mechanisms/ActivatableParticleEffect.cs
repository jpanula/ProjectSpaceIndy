using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableParticleEffect : MonoBehaviour
{
    public ActivatorBase[] Activators;
    public ParticleSystem ParticleEffect;
    public float ActivationTime;
    protected bool Activated;
    private float _activationTimer;

    public virtual void Activate()
    {
        var t = transform;
        Instantiate(ParticleEffect.gameObject, t.position, t.rotation);
    }
    
    public virtual bool IsActivated()
    {
        foreach (var activator in Activators)
        {
            if (!activator.Active)
            {
                return false;
            }
        }

        return true;
    }

    protected virtual void Update()
    {
        if (IsActivated() && !Activated && _activationTimer >= ActivationTime)
        {
            Activate();
            Activated = true;
        }

        _activationTimer += TimerManager.Instance.GameDeltaTime;
    }
}
