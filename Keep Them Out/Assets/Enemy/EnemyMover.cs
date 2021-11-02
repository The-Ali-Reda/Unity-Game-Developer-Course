using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    private List<Waypoint> path = new List<Waypoint>();
    [SerializeField]
    private float _waitTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }
    IEnumerator FollowPath()
    {
        foreach(var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(_waitTime);
        }
    }
}
