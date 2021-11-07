using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _startCoordinates;
    public Vector2Int StartCoordinates { get { return _startCoordinates; } }
    [SerializeField]
    private Vector2Int _destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return _destinationCoordinates; } }

    Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> _frontier = new Queue<Node>();
    // Start is called before the first frame update
    private Node _currentSearchNode;
    private Node _startNode;
    private Node _endNode;
    private Vector2Int[] _directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    private GridManager _gridManager;
    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
    }
    private void Start()
    {
        _startNode = _gridManager.GetNode(_startCoordinates);
        _endNode = _gridManager.GetNode(_destinationCoordinates);
        _startNode.IsWalkable = true;
        _endNode.IsWalkable = true;
        BreadthFirstSearch(_startCoordinates);
        BuildPath();
    }
    private List<Node> ExploreNeighbours()
    {
        var neighbours = new List<Node>();
        var searchCoords = _currentSearchNode.Coordinates;
        for(int i = 0; i < _directions.Length; i++)
        {
            var gridNode = _gridManager.GetNode(searchCoords + _directions[i]);
            if (gridNode != null && (gridNode.IsWalkable|| gridNode.Coordinates == _startCoordinates || gridNode.Coordinates == _destinationCoordinates) && !gridNode.IsExplored) {
                gridNode.ConnectedTo = _currentSearchNode;
                neighbours.Add(gridNode);
            }
        }
        return neighbours;
    }
    private void BreadthFirstSearch(Vector2Int coordinates)
    {
        _frontier.Clear();
        _reached.Clear();
        bool isRunning = true;
        _frontier.Enqueue(_gridManager.GetNode(coordinates));
        _reached[_startCoordinates] = _gridManager.GetNode(coordinates);
        while (_frontier.Count > 0 && isRunning)
        {
            _currentSearchNode = _frontier.Dequeue();
            _currentSearchNode.IsExplored = true;   
            var neighbours = ExploreNeighbours();
            if (_currentSearchNode.Coordinates == _destinationCoordinates)
                isRunning = false;
            foreach (var neighbour in neighbours)
                _frontier.Enqueue(neighbour);
        }
    }
    private List<Node> BuildPath()
    {
        var path = new List<Node>();
        Node curr = _endNode;
        curr.IsPath = true;
        path.Add(curr);
        while (curr.ConnectedTo != null)
        {
            curr = curr.ConnectedTo;
            curr.IsPath = true;
            path.Add(curr);
        }
        path.Reverse();
        return path;
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(_startCoordinates);
    }
    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        _gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }
    public bool WillBlockPath(Vector2Int coordinates)
    {
        var node = _gridManager.GetNode(coordinates);
        if (node != null)
        {
            var prevState = node.IsWalkable;
            node.IsWalkable = false;
            List<Node> newPath = GetNewPath();
            node.IsWalkable = prevState;
            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }
    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath",false, SendMessageOptions.DontRequireReceiver);
    }
}
