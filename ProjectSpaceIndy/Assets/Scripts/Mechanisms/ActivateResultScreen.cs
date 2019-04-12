using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateResultScreen : MonoBehaviour
{
    public ActivatorBase PlayerDetector;

    private void Update()
    {
        if (PlayerDetector.Active)
        {
            MenuManager.Instance.ShowResultScreen(true);
        }
    }
}
