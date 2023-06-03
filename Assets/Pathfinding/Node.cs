using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable; //If a node is reachable by an enemy
    public bool isExplored; //If a node is reached and explored
    public bool isPath; //If a node is a part of the path that is generated for that frame
    public Node connectedTo; //The node where this node was reached from

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
        isExplored = false;
        isPath = false;
        connectedTo = null;
    }
}