using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;
    [SerializeField]
    private TMP_Text _scoreText;
    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        _scoreText.text = $"score: {score}";
    }
}
