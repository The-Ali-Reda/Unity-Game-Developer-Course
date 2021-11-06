using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _gridSize; //for both width and height
    [SerializeField]
    [Tooltip("This should match the unity grid snap size")]
    private int _unityGridSize = 10;
    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return _grid; } }
    public int UnityGridSize { get { return _unityGridSize; } }
    private void Awake()
    {
        CreateGrid();
    }
    public Node GetNode(Vector2Int coords)
    {
        if (coords == null || !_grid.ContainsKey(coords))
            return null;
        return _grid[coords];
    }
    private void Start()
    {
        
    }
    private void CreateGrid()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                _grid[coordinates] = new Node(coordinates, true);
            }
        }
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            Debug.Log($"Blocking {coordinates}");
            Grid[coordinates].IsWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(var entry in _grid)
        {
            entry.Value.ConnectedTo = null;
            entry.Value.IsExplored = false;
            entry.Value.IsPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / _unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / _unityGridSize);
        return coordinates;
    }
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.y = 0;
        position.x = coordinates.x * _unityGridSize;
        position.z = coordinates.y * _unityGridSize;
        return position;
    }
}
