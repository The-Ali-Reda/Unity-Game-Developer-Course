using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    private Color _defaultColor = Color.white;
    [SerializeField]
    private Color _blockedColor = Color.red;

    TextMeshPro _label;
    Vector2Int _coordinates = new Vector2Int();
    Waypoint _waypoint;
    private void Awake()
    {
        _waypoint = GetComponentInParent<Waypoint>();
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
        ColorCoordinates();
        if (Input.GetKeyDown(KeyCode.C))
            ToggleLabels();
    }
    void ColorCoordinates()
    {
        if (_waypoint.IsPlaceable)
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
        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x/ UnityEditor.EditorSnapSettings.move.x);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z/ UnityEditor.EditorSnapSettings.move.z);
        _label.text = $"{_coordinates.x},{_coordinates.y}";
    }
    void UpdateObjectName()
    {
        transform.parent.name = $"{_coordinates.ToString()}";
    }
}
