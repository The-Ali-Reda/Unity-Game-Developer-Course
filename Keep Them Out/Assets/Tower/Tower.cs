using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private int _cost = 50;
    private Bank _bank;
    private Bank bank
    {
        get
        {
            if (_bank == null)
            {
                _bank = FindObjectOfType<Bank>();
            }
            return _bank;
        }
    }
    public bool CreateTower(Vector3 position)
    {
        
        Debug.Log($"Is bank null: {bank==null}");
        if (bank == null || (bank.CurrentBalance < _cost))
            return false;
        bank.Withdraw(_cost);
        var createdObject = Instantiate(gameObject, position, Quaternion.identity);
        return true;
    }
}
