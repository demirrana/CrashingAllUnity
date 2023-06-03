using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; //128

public class Bank : MonoBehaviour
{
    [Tooltip("The amount of money we have at the start of the game.")]
    [SerializeField] int startingBalance = 150;

    int currentBalance;
    public int CurrentBalance { get { return currentBalance; } } //Other classes need to access current balance.

    [SerializeField] TextMeshProUGUI displayBalance;

    void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();
        
        if (currentBalance < 0)
        {
            //Lose the game
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    void UpdateDisplay() //Updates the current balance on the screen.
    {
        displayBalance.text = "Bank: " + currentBalance;
    }
}
