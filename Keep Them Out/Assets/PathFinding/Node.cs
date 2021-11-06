using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int Coordinates;
    public bool IsWalkable;
    public bool IsExplored;
    public bool IsPath;
    public Node ConnectedTo;

    public Node(Vector2Int Coordinates, bool IsWalkable)
    {
        this.Coordinates = Coordinates;
        this.IsWalkable = IsWalkable;
    }
}
