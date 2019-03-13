using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public int Zeros;
    private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        string score = "<mspace=2.2em>";
        for (int i = 0; i < Zeros - Mathf.Max(Mathf.FloorToInt(Mathf.Log10(GameManager.Score)), 0) - 1; i++)
        {
            score += "0";
        }

        score += GameManager.Score;
        _scoreText.text = score;
    }
}
