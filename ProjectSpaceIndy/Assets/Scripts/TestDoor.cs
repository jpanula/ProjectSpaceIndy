using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MechanismBase
{
    private void Update()
    {
        if (Activation)
        {
            Debug.Log("OVI AUKI!");
        }
    }
}
