using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Time;
    private double _gametimeInSeconds;
    private double _gametimeInMinutes;
    private double _gametimeInHours;
    
    public delegate void SceneChangeHandler();
    public static event SceneChangeHandler SceneChanging;

    private void OnEnable()
    {
        Score.text = "Score: " + GameManager.Score;

        _gametimeInSeconds = (Math.Floor(TimerManager.Instance.ScaledGameTime));
        if (_gametimeInSeconds > 60)
        {
            _gametimeInMinutes = Math.Floor(_gametimeInSeconds / 60);
        }

        if (_gametimeInMinutes > 60)
        {
            _gametimeInHours = Math.Floor(_gametimeInMinutes / 60);
        }
        
        Debug.Log(_gametimeInSeconds);
        
        if (_gametimeInMinutes > 0)
        {
            Time.text = "Time: " + _gametimeInMinutes + " min " + (_gametimeInSeconds - (60 * _gametimeInMinutes)) + " sec";
        }
        else if (_gametimeInHours > 0)
        {
            Time.text = "Time: " + _gametimeInHours + " h " + (_gametimeInMinutes - (60 * _gametimeInHours)) + " m " + (_gametimeInSeconds - (60 * _gametimeInMinutes)) + " s";
        }
        else
        { Time.text = "Time: " + _gametimeInSeconds + " seconds"; }

        TimerManager.Instance.SetGameTimeScale(0);
    }

    public void LevelSelect()
    {
        if (SceneChanging != null)
        {
            SceneChanging.Invoke();
        }
        SceneManager.LoadScene(0);
        TimerManager.Instance.SetGameTimeScale(1);
        GameManager.EscapePhase = false;
        GameManager.Score = 0;
        GameManager.Instance.CurrentLevel = GameManager.Level.MainMenu;
        MenuManager.Instance.ShowResultScreen(false);
        MenuManager.Instance.ShowMainMenu(true);
    }

    public void Retry()
    {
        if (SceneChanging != null)
        {
            SceneChanging.Invoke();
        }
        SceneManager.LoadScene(2);
        TimerManager.Instance.SetGameTimeScale(1);
        MenuManager.Instance.ShowResultScreen(false);
        GameManager.EscapePhase = false;
        GameManager.Score = 0;
    }
}
