using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
}
