using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable = false;
    [SerializeField] GameObject _towerPrefab;
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            isPlaceable = false;
            Instantiate(_towerPrefab, transform.position, Quaternion.identity);
        }
    }
}
