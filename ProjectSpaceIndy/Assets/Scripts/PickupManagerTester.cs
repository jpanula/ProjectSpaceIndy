using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManagerTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PickupBase scrap = PickupManager.Instance.GetScrap();
            scrap.transform.position = transform.position;
        }
    }
}
