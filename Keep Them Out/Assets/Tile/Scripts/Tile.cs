using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] 
    private bool isPlaceable = false;
    public bool IsPlaceable
    {
        get
        {
            return isPlaceable;
        }
    }
    [SerializeField] 
    private Tower _towerPrefab;

    private GridManager _gridManager;
    private Vector2Int _coordinates = new Vector2Int();
    private PathFinder _pathFinder;
    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _pathFinder = FindObjectOfType<PathFinder>();
    }
    /**
     * known issue:: because blocking is done in start and path finder is finding the path in start
     * the order is often messed up causing it to find path before node is actually blocked
     */
    private void Start()
    {
        if (_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable)
            {
                _gridManager.BlockNode(_coordinates);
            }
        }
    }
    private void OnMouseDown()
    {
        var node = _gridManager.GetNode(_coordinates);
        if (_gridManager!= null && node.IsWalkable && !_pathFinder.WillBlockPath(_coordinates))
        {
            var isCreated = _towerPrefab.CreateTower(transform.position);
            if (isCreated)
            {
                isPlaceable = false;
                _gridManager.BlockNode(_coordinates);
                _pathFinder.NotifyReceivers();
            }
        }
    }
}
