using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _goldReward = 25;
    [SerializeField]
    private int _goldPenalty = 25;
    private Bank _bank;
    // Start is called before the first frame update
    private void Start()
    {
        _bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        _bank?.Deposit(_goldReward);
    }
    public void StealGold()
    {
        _bank?.Withdraw(_goldPenalty);
    }
}
