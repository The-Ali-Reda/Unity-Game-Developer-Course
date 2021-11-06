using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField]
    [Range(0,5)]
    private float _speed = 1f;

    [SerializeField]
    [Range(0, 5)]
    private float _rotationSpeed = 1f;
    
    private List<Node> path = new List<Node>();
    private GameObject enemyMesh;
    private Enemy _enemy;
    private GridManager _gridManager;
    private PathFinder _pathFinder;
    // Start is called before the first frame update
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        enemyMesh = GetComponentInChildren<MeshRenderer>().gameObject;
        _gridManager = FindObjectOfType<GridManager>();
        _pathFinder = FindObjectOfType<PathFinder>();
    }
    private void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }
    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coords = new Vector2Int();
        StopAllCoroutines();
        path.Clear();
        if (resetPath)
            coords = _pathFinder.StartCoordinates;
        else
            coords = _gridManager.GetCoordinatesFromPosition(transform.position);
        path = _pathFinder.GetNewPath(coords);
        StartCoroutine(FollowPath());

    }
    private void ReturnToStart()
    {
        transform.position = _gridManager.GetPositionFromCoordinates(_pathFinder.StartCoordinates);
    }
    private IEnumerator FollowPath()
    {
        for (int i= 1; i < path.Count; i++)
        {

            var waypoint = path[i];
            var startPosition = transform.position;
            var endPosition = _gridManager.GetPositionFromCoordinates(waypoint.Coordinates);
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
                    lerpPercent += Time.deltaTime * _rotationSpeed;
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
        FinishPath();
    }
    private void FinishPath()
    {
        _enemy.StealGold();
        gameObject.SetActive(false);
    }
}
