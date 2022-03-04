using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int score;

    private void Awake()
    {
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    private void Start()
    {
        printScore();
    }

    void printScore()
    {
        scoreText.text = $"Score: {score:0000}";
    }

    public void UpdatScore(int _score)
    {
        score += _score;
        printScore();
    }
}
