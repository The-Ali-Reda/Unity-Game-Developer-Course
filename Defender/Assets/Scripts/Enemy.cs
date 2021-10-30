using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _deathVFX;
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private int _score = 10;

    private bool _isDead = false;
    private ScoreBoard _scoreBoard;

    private void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($" {name} hit by {other.gameObject.name}");
        GameObject vfx = Instantiate(_deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = _parent;
        if (!_isDead)
        {
            _isDead = true;
            _scoreBoard.IncreaseScore(_score);
        }
        Destroy(gameObject);
    }
}
