using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro _label;
    Vector2Int _coordinates = new Vector2Int();
    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
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
