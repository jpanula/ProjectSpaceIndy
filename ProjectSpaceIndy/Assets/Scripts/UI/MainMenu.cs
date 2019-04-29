using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void ShowSettings()
    {
        MenuManager.Instance.ShowMainMenu(false);
        MenuManager.Instance.ShowSettingsMenu(true);
    }

    public void ShowLevelSelect()
    {
        MenuManager.Instance.ShowMainMenu(false);
        MenuManager.Instance.ShowLevelSelect(true);
    }
    
    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
