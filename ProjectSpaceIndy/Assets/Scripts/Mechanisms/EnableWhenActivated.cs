using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWhenActivated : MonoBehaviour
{
    public ActivatorBase Activator;
    public GameObject EnableThis;
    void Update()
    {
        if (Activator.Active)
        {
            EnableThis.SetActive(true);
        }
    }
}
