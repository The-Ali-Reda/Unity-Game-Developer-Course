using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    private List<Waypoint> path = new List<Waypoint>();
    [SerializeField]
    [Range(0,5)]
    private float _speed = 1f;
    private GameObject enemyMesh;
    private Enemy _enemy;
    // Start is called before the first frame update
    void Awake()
    {
        _enemy = GetComponent<Enemy>();
        enemyMesh = GetComponentInChildren<MeshRenderer>().gameObject;
    }
    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }
    void FindPath()
    {
        path.Clear();
        var waypoints = GameObject.FindGameObjectsWithTag("Path");
        foreach(var waypoint in waypoints)
        {
            var waypointComponent = waypoint.GetComponent<Waypoint>();
            path.Add(waypointComponent);
        }

    }
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    IEnumerator FollowPath()
    {
        foreach(var waypoint in path)
        {
            var startPosition = transform.position;
            var endPosition = waypoint.transform.position;
            float travelPercent = 0f;
            //transform.LookAt looks instantly at the target, the code in the region below is for smooth rotation at corners
            //transform.LookAt(endPosition);
            #region Handle Rotation Smoothly
            var lerpPercent = 0f;
            var direction = endPosition - startPosition;
            var endRotation = Quaternion.LookRotation(direction);
            var startRotation = enemyMesh.transform.rotation;
            if (enemyMesh.transform.forward != direction.normalized)
            {
                while (lerpPercent < 1)
                {
                    lerpPercent += Time.deltaTime * _speed;
                    enemyMesh.transform.rotation = Quaternion.Lerp(startRotation, endRotation, lerpPercent);
                    yield return null;
                }
            }
            #endregion
            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                /**WaitForEndOfFrame doesn't run in scene view even when unpaused
                * the difference between them is **when** it is executed
                * return null is executed after update is done
                * return new WaitForEndOfFrame is executed after everything is done (including ui, cameras and other stuff)
                * In this use case, there is no real difference in behavior
                **/
                yield return null;//new WaitForEndOfFrame();
            }
            
        }
        _enemy.StealGold();
        gameObject.SetActive(false);
    }
}
