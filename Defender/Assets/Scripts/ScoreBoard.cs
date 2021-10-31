using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public int Score { get; private set; } = 0;
    [SerializeField]
    private TMP_Text _scoreText;
    public void IncreaseScore(int amountToIncrease)
    {
        Score += amountToIncrease;
        _scoreText.text = $"score: {Score}";
    }
}
