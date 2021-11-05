using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    [Range(1,10)]
    private int _maxHitpoints = 5;

    [SerializeField]
    [Tooltip("Adds amount to max HP when enemy dies")]
    private int _difficultyRamp = 1;
    int currentHitpoints = 0;

    private Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        currentHitpoints = _maxHitpoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitpoints--;
        if (currentHitpoints <= 0)
        {
            _enemy.RewardGold();
            gameObject.SetActive(false);
            _maxHitpoints += _difficultyRamp;
        }
    }
}
