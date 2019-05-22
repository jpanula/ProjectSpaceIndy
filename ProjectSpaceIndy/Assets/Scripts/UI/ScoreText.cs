using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public int Zeros;
    public float Speed = 2;
    public TextMeshProUGUI ComboValues;
    public TextMeshProUGUI ScoreMultiplier;
    private TextMeshProUGUI _scoreText;
    private float _score;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _score = Mathf.Lerp(_score, ScoreManager.Score,
            TimerManager.Instance.UiDeltaTime * Speed * (1 + Mathf.Log10(Mathf.Abs(ScoreManager.Score - _score))));
        
        string score = "<mspace=2.2em>";
        for (int i = 0; i < Zeros - Mathf.Max(Mathf.FloorToInt(Mathf.Log10(_score)), 0) - 1; i++)
        {
            score += "0";
        }

        score += (int) _score;
        _scoreText.text = score;

        ScoreMultiplier.text = "x" + ScoreManager.ComboMultiplier.ToString("0.00");
        ComboValues.text = ScoreManager.Combo + "\n" + Mathf.Max(ScoreManager.ComboTimeLeft, 0).ToString("0.00");
    }
}
