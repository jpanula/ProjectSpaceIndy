using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        MenuManager.Instance.ShowSettingsMenu(false);
        MenuManager.Instance.ShowMainMenu(true);
    }
}
