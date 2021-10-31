using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _deathFX;
    [SerializeField]
    private GameObject _hitVFX;
    [SerializeField]
    private Transform _vfxParent;
    [SerializeField]
    private int _score = 10;
    [SerializeField]
    private int _hitPoints = 100;

    private bool _isDead = false;
    private ScoreBoard _scoreBoard;
    private void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
        _vfxParent = GameObject.FindGameObjectWithTag("SpawnAtRuntime")?.transform;
        AddRigidBody();
        
    }
    private void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (_isDead)
            return;
        Debug.Log($" {name} hit by {other.gameObject.name}");

        TakeDamage(10);
        if (_hitPoints <= 0)
        {
            Die();
        }
    }
    private void TakeDamage(int damageAmount)
    {
        _hitPoints -= damageAmount;
        PlayHitVFX();
    }
    private void Die()
    {
        _isDead = true;
        _scoreBoard.IncreaseScore(_score);
        PlayDeathFX();
        Destroy(gameObject);
    }
    private void PlayDeathFX()
    {
        GameObject fx = Instantiate(_deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = _vfxParent;
    }
    private void PlayHitVFX()
    {
        GameObject vfx = Instantiate(_hitVFX, transform.position, Quaternion.identity);
    }
}
