using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static Text _scoreText;
    private static uint _score;

    public static uint GetScore()
    {
        return _score;
    }

    public static void AddScore(uint newScore)
    {
        _score += newScore;
        Text _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        _scoreText.text = _score.ToString();
    }

}
