using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
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
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            Debug.Log("in mouse down");
            var isCreated = _towerPrefab.CreateTower(transform.position);
            isPlaceable = !isCreated;
            
        }
    }
}
