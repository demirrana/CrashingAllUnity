using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [Tooltip("The amount of hits that enemy needs to take in order it to die.")]
    [SerializeField] int maxHitPoints = 7;
    [Tooltip("Adds amount to the maximum hit points when an enemy dies.")]
    [SerializeField] int difficultyRamp = 1;
    
    int currentHitPoints = 0;

    Enemy enemy; //Enemy class is needed in order to apply Bank operations.

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other) 
    {
        HitProcess();
    }

    void HitProcess() //Manages the bank operations and vanishing the enemy.
    {
        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false); //Each enemy object is reused, so it is not destroyed since it will be used again.
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }        
    }
}
