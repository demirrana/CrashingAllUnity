using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("The amount of money that is earned if an enemy dies.")]
    [SerializeField] int rewardGolds = 25;
    [Tooltip("The amount of money that is lost if an enemy manages to reach to its palace.")]
    [SerializeField] int penaltyGolds = 25;

    Bank bank;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null) { return; }
        bank.Deposit(rewardGolds);
    }

    public void StealGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(penaltyGolds);
    }
}
