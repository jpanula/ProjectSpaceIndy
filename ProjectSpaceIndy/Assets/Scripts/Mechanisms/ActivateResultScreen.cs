using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateResultScreen : MonoBehaviour
{
    public ActivatorBase PlayerDetector;
    private bool _activated;

    private void Update()
    {
        if (PlayerDetector.Active && !_activated)
        {
            MenuManager.Instance.ShowResultScreen(true);
            _activated = true;
        }
    }
}
