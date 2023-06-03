using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerPrice = 30;
    [SerializeField] float delayTime = 0.6f;

    void Start()
    {
        StartCoroutine(Build()); //The creation of the towers
    }

    IEnumerator Build() //Creates the towers by generating the bottom part first, then the top part after a while
    {
        foreach (Transform child in transform) //First all the parts are deactivated
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child) //Since a component of the tower has a particle system as the component's child
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform) //They are activated in turn
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayTime);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (towerPrice <= bank.CurrentBalance)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(towerPrice);
            return true;
        }
        
        return false;
    }
}
