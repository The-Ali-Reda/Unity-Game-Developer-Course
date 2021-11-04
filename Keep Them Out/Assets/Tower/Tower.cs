using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private int _cost = 50;
    private Bank _bank;
    public bool CreateTower(Vector3 position)
    {
        // in an actual project this would be in some sort of manager object rather than here
        _bank = FindObjectOfType<Bank>();
        if (_bank == null || (_bank.CurrentBalance < _cost))
            return false;
        _bank.Withdraw(_cost);
        var createdObject = Instantiate(gameObject, position, Quaternion.identity);
        return true;
    }
}
