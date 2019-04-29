using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public VolumeAdjuster VolumeAdjuster;
    public DeadzoneAdjuster DeadzoneAdjuster;
    public void BackToMainMenu()
    {
        MenuManager.Instance.ShowSettingsMenu(false);
        MenuManager.Instance.ShowMainMenu(true);
    }

    public void UpdateFieldsAndSliders()
    {
        VolumeAdjuster.UpdateFields();
        VolumeAdjuster.UpdateSliders();
        DeadzoneAdjuster.UpdateFields();
        DeadzoneAdjuster.UpdateSliders();
    }

    public void RestoreDefaults()
    {
        VolumeAdjuster.ResetVolume();
        DeadzoneAdjuster.ResetStickDeadzones();
    }
}
