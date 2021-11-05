using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField]
    private int _startingBalance = 150;
    [SerializeField]
    private TextMeshProUGUI _displayBalance;
    private int _currentBalance;
    public int CurrentBalance
    {
        get
        {
            return _currentBalance;
        }
    }

    private void Awake()
    {
        _currentBalance = _startingBalance;
    }
    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }
    public void Withdraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();
        if (_currentBalance < 0)
        {
            //Lose the game
            var idx = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(idx);
        }
    }
    private void UpdateDisplay()
    {
        _displayBalance.text = $"Gold: {_currentBalance}";
    }
}
