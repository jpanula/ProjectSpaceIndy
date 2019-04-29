using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void GoToLevel(int levelNumber)
    {

        SceneManager.LoadScene(levelNumber + 1);
    }

    public void BackToMainMenu()
    {
        MenuManager.Instance.ShowLevelSelect(false);
        MenuManager.Instance.ShowMainMenu(true);
    }

}
