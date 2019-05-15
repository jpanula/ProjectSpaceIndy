using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public delegate void BackToMainHandler();

    public static event BackToMainHandler OnBackToMainMenu;
    
    public void ReturnToMainMenu()
    {
        if (OnBackToMainMenu != null)
        {
            OnBackToMainMenu.Invoke();
        }
        SceneManager.LoadScene(0);
        GameManager.Score = 0;
        GameManager.Instance.CurrentLevel = GameManager.Level.MainMenu;
        MenuManager.Instance.ShowPauseMenu(false);
        MenuManager.Instance.ShowMainMenu(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        MenuManager.Instance.ShowPauseMenu(false);
        
    }

    public void OpenSettings()
    {
        MenuManager.Instance.ShowPauseMenuKeepTimeScale(false);
        MenuManager.Instance.ShowSettingsMenu(true);
    }
}
