using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshPro kullanabilmek için (112)

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white; //Tower placeable node color
    [SerializeField] Color blockedColor = Color.gray; //Nodes that towers cannot be placed on color
    [SerializeField] Color exploredColor = Color.cyan; //Explored node color
    [SerializeField] Color pathColor = Color.magenta; //A part of enemy path color

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int(0,0); //The node's coordinate (as it is moved, it gets updated)
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();

        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying) //In the adjusting scene mode, as a tile is moved, its name and displayed coordinates are updated
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        SetLabelColor();

        ToggleCoordinates();
    }

    void DisplayCoordinates() //Displays the coordinates of the nodes of the grid
    {
        if (gridManager == null) { return; }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName() //Makes the object's name same as its coordinates
    {
        transform.parent.name = coordinates.ToString();
    }

    void SetLabelColor() //Arranges the colors of the nodes according to the path finding operation
    {
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void ToggleCoordinates() //Making the coordinates seen or hidden (by pressing C)
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }
}
