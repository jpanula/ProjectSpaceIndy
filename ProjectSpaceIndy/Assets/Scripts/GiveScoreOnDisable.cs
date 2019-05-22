using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveScoreOnDisable : MonoBehaviour
{
    public int ComboAmount;
    public int ScoreAmount;
    private bool _isQuitting;

    private void OnEnable()
    {
        ResultScreen.SceneChanging += OnSceneChanging;
        PauseMenu.OnBackToMainMenu += OnSceneChanging;
    }
    private void OnDisable()
    {
        ResultScreen.SceneChanging -= OnSceneChanging;
        PauseMenu.OnBackToMainMenu -= OnSceneChanging;
        if (!_isQuitting)
        {
            ScoreManager.Combo += ComboAmount;
            ScoreManager.Score += ScoreAmount;
        }
    }

    private void OnSceneChanging()
    {
        _isQuitting = true;
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }
}
