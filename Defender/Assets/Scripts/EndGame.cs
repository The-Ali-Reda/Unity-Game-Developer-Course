using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private ScoreBoard _scoreBoard;
    [SerializeField]
    private TMP_Text _endText;
    void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
        
    }
    private void OnEnable()
    {
        _endText.text = $"You WIN!!!!! \n you have scored {_scoreBoard?.Score} points. \n Press R to reset the game.\n Press ESC to quit.";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }
    private void ReloadLevel()
    {
        var idx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idx);
    }
}
