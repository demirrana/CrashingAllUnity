using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower tower; //Tower prefab that is gonna be placed

    [SerializeField] bool isPlaceable; //If that node is clickable (to place a tower)
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int(); //The current node's coordinates

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates)) //If an enemy's path will not be blocked by placing a tower
        {
            bool isSuccessful = tower.CreateTower(tower, transform.position);
            if (isSuccessful) //If a tower is placeable
            {
                gridManager.BlockNode(coordinates); //No other towers can be placed
                pathfinder.NotifyReceivers(); //To prevent enemies from crossing this node
            }
        }
    }
}