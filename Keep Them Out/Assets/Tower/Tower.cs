using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private int _cost = 50;
    [SerializeField]
    private float _delay = 0.5f;
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
    private void Start()
    {
        StartCoroutine(Build());
    }
    private IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(_delay);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
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
