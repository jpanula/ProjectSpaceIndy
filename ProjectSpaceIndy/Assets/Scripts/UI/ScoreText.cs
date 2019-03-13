using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _scoreText.text = "Score\n" + GameManager.Score;
    }
}
