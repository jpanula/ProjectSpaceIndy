using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public VolumeAdjuster VolumeAdjuster;
    public DeadzoneAdjuster DeadzoneAdjuster;
    public TMP_Text BackText;
    public GameObject Background;

    private void OnEnable()
    {
        if (GameManager.Instance.CurrentLevel == GameManager.Level.MainMenu)
        {
            Background.SetActive(false);
            BackText.text = "Back to Main Menu";
        }
        else
        {
            Background.SetActive(true);
            BackText.text = "Back to Pause Menu";
        }
    }

    public void BackToMainMenu()
    {
        MenuManager.Instance.ShowSettingsMenu(false);
        if (GameManager.Instance.CurrentLevel == GameManager.Level.MainMenu)
        {
            MenuManager.Instance.ShowMainMenu(true);
        }
        else
        {
            MenuManager.Instance.ShowPauseMenuKeepTimeScale(true);
        }
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
