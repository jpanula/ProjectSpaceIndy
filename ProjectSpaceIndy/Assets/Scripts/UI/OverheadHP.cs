﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheadHP : MonoBehaviour
{
    public Health ParentHealth;
    public float HealthBarChangeSpeed;
    public RotationType Rotation = RotationType.CameraFacing;
    public Camera CameraToFace;
    [Space(15)]
    public Image HealthBarFill;

    public enum RotationType
    {
        None,
        Locked, 
        CameraFacing
    }

    private void Awake()
    {
        if (CameraToFace == null)
        {
            CameraToFace = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }

    private void Update()
    {
        var healthRatio = (float) ParentHealth.CurrentHealth / ParentHealth.MaxHealth;
        HealthBarFill.fillAmount = Mathf.Lerp(HealthBarFill.fillAmount, healthRatio, TimerManager.Instance.UiDeltaTime * HealthBarChangeSpeed);
        switch (Rotation)
        {
                case RotationType.None:
                    break;
                case RotationType.Locked:
                    transform.rotation = Quaternion.identity;
                    break;
                case RotationType.CameraFacing:
                    transform.rotation = Quaternion.LookRotation(CameraToFace.transform.forward, CameraToFace.transform.up);
                    break;
        }
    }
}
