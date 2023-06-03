using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class makes use of enemy objects by reusing them
public class ObjectPool : MonoBehaviour
{
    [Tooltip("The enemy model that is used in the game.")]
    [SerializeField] GameObject enemyPrefab;
    [Tooltip("The count of enemy prefabs that will be reused. (Amount of enemies that we can see in the same frame.)")]
    [SerializeField] [Range(0,50)] int poolSize = 5;
    [Tooltip("The amount of seconds that is between cloning events.")]
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;

    GameObject[] pool; //The enemies are held in this

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool() //Fills the pool with enemies that are deactive at first.
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemy() //As the enemies reach their destination or die, this method spawns them from the starting point and makes them active.
    {
        while (true)
        {
            EnableObjectInPool();

            yield return new WaitForSeconds(spawnTimer);
        }
    }

    void EnableObjectInPool() //Activates each enemy object if they are deactived.
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}