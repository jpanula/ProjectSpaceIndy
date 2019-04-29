using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public enum Menu
    {
        None = 0,
        Main,
        Pause,
        Settings,
        LevelSelect,
        Title,
        ResultScreen
    }
    
    public static MenuManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject PauseMenu;
    public GameObject ResultScreen;
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject LevelSelect;

    private Menu _currentMenu;
    private float _gameTimeScale;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            switch (_currentMenu)
            {
                case Menu.None:
                    ShowPauseMenu(true);
                    break;
                case Menu.Pause:
                    ShowPauseMenu(false);
                    break;
                case Menu.Settings:
                    ShowMainMenu(true);
                    break;
                case Menu.LevelSelect:
                    ShowMainMenu(true);
                    break;
            }
        }
    }

    public void ShowPauseMenu(bool show)
    {
        if (show)
        {
            _currentMenu = Menu.Pause;
            PauseMenu.SetActive(true);
            _gameTimeScale = TimerManager.Instance.GameDeltaScale;
            TimerManager.Instance.SetGameTimeScale(0);
        }
        else
        {
            _currentMenu = Menu.None;
            PauseMenu.SetActive(false);
            TimerManager.Instance.SetGameTimeScale(_gameTimeScale);
        }
    }

    public void ShowMainMenu(bool show)
    {
        if (show)
        {
            _currentMenu = Menu.Main;
            MainMenu.SetActive(true);
        }
        else
        {
            _currentMenu = Menu.None;
            MainMenu.SetActive(false);
        }
    }

    public void ShowSettingsMenu(bool show)
    {
        if (show)
        {
            _currentMenu = Menu.Settings;
            SettingsMenu.SetActive(true);
        }
        else
        {
            _currentMenu = Menu.None;
            SettingsMenu.SetActive(false);
        }
    }

    public void ShowLevelSelect(bool show)
    {
        if (show)
        {
            _currentMenu = Menu.LevelSelect;
            LevelSelect.SetActive(true);
        }
        else
        {
            _currentMenu = Menu.None;
            LevelSelect.SetActive(false);
        }
    }

    public void ShowTitleMenu(bool show)
    {
        
    }

    public void ShowResultScreen(bool show)
    {
        if (show)
        {
            _currentMenu = Menu.ResultScreen;
            ResultScreen.SetActive(true);
            _gameTimeScale = TimerManager.Instance.GameDeltaScale;
            TimerManager.Instance.SetGameTimeScale(0);
        }
        else
        {
            _currentMenu = Menu.None;
            ResultScreen.SetActive(false);
            TimerManager.Instance.SetGameTimeScale(_gameTimeScale);
        }
    }
}
