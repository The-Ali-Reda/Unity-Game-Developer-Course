using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    private Color _defaultColor = Color.white;
    [SerializeField]
    private Color _blockedColor = Color.red;
    [SerializeField]
    private Color _exploredColor = Color.blue;
    [SerializeField]
    private Color _pathColor = Color.yellow;

    TextMeshPro _label;
    Vector2Int _coordinates = new Vector2Int();
    GridManager _gridManager;
    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _label = GetComponent<TextMeshPro>();
        _label.enabled = false;
        DisplayCoordinates(); //to run the script once (per tile) in runtime
    }
    // Update is called once per frame
    void Update()
    {
        //only update at edit time
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        SetLabelColor();
        if (Input.GetKeyDown(KeyCode.C))
            ToggleLabels();
    }
    void SetLabelColor()
    {
        var node = _gridManager?.GetNode(_coordinates);
        if (node == null)
            return;
        if (node.IsPath)
        {
            _label.color = _pathColor;
        } else if(node.IsExplored)
        {
            _label.color = _exploredColor;
        }
        else if (node.IsWalkable)
        {
            _label.color = _defaultColor;
        } else
        {
            _label.color = _blockedColor;
        }
    }
    void ToggleLabels()
    {
        _label.enabled = !_label.enabled;
    }
    void DisplayCoordinates()
    {
        if (_gridManager == null)
            return;
        _coordinates = _gridManager.GetCoordinatesFromPosition(transform.parent.position);
        _label.text = $"{_coordinates.x},{_coordinates.y}";
    }
    void UpdateObjectName()
    {
        transform.parent.name = $"{_coordinates}";
    }
}
