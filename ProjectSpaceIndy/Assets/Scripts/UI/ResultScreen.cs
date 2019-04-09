using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Time;

    private void OnEnable()
    {
        Score.text = GameManager.Score.ToString();
        Time.text = TimerManager.Instance.ScaledGameTime.ToString();
        TimerManager.Instance.SetGameTimeScale(0);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
        GameManager.EscapePhase = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene(2);
        gameObject.SetActive((false));
        GameManager.EscapePhase = false;
    }
}
