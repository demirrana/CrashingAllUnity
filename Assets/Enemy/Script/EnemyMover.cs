using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [Tooltip("The multiplier that manages the speed of the enemy object.")]
    [SerializeField] [Range(0f,5f)] float speedFactor;

    List<Node> path = new List<Node>();

    Enemy enemy;
    Pathfinder pathfinder;
    GridManager gridManager;

    void OnEnable() //Whenever an enemy object is activated, it will start from the zero point and compute its path according to the new environment.
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
    }

    IEnumerator MoveInRoad() //Enemy's movement is executed
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercentage = 0f;

            transform.LookAt(endPosition);

            while (travelPercentage < 1f) //Until the enemy reaches its destination node
            {
                travelPercentage += Time.deltaTime * speedFactor;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercentage);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishRoad(); //The case of enemy reaching its palace
    }

    private void FinishRoad()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    void RecalculatePath(bool resetPath) //Calculates the path and if resetPath is true, the enemy starts from the place where it was left.
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear(); //Since the path will be computed from scratch
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(MoveInRoad()); //New coroutine starts after the path is computed
    }

    void ReturnToStart() //Enemy returns to the starting place.
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }
}