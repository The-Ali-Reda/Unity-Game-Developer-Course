using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _delay = 3f;
    [SerializeField]
    private int _poolSize = 5;
    List<GameObject> pool;

    private void Awake()
    {
        pool = new List<GameObject>(_poolSize);
        PopulatePool();
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    private void PopulatePool()
    {
        for(int i=0; i<_poolSize; i++)
        {
            var tempEnemy = Instantiate(_enemyPrefab, transform);
            tempEnemy.SetActive(false);
            pool.Add(tempEnemy);
        }
    }
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(_delay);
        }
    }
    private void EnableObjectInPool()
    {
        //bool enabled = false;
        foreach (var enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enabled = true;
                enemy.SetActive(true);
                break;
            }    
        }
        /*if (!enabled)
        {
            var tempEnemy = Instantiate(_enemyPrefab, transform);
            pool.Add(tempEnemy);
            _poolSize = pool.Count;
        }*/
    }
}
