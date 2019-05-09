using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void GoToLevel(int levelNumber)
    {
        GameManager.Instance.CurrentLevel = GameManager.Level.Test;
        SceneManager.LoadScene(levelNumber + 1);
        TimerManager.Instance.SetScaledSceneLoadTime();
        MenuManager.Instance.ShowLevelSelect(false);
        
    }

    public void BackToMainMenu()
    {
        MenuManager.Instance.ShowLevelSelect(false);
        MenuManager.Instance.ShowMainMenu(true);
    }

}
