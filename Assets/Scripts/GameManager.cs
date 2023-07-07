using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private int _score;

    private void Start()
    {
        _score = 0;
        UpdateScoreText();
    }
    
    public void UpdateScore(int dScore)
    {
        _score += dScore;
        UpdateScoreText();
    }
    
    private void UpdateScoreText() => scoreText.text = $"Score: {_score}";
}
