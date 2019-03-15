using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectOnDestroy : MonoBehaviour
{
    public ParticleSystem Effect;

    private void OnDestroy()
    {
        GameObject effectObject = Instantiate(Effect.gameObject, transform.position, transform.rotation);
        var main = effectObject.GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Destroy;
    }
}
