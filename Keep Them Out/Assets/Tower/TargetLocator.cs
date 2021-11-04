using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] 
    private Transform _weapon;
    [SerializeField]
    private float _range = 15f;
    [SerializeField]
    [Range(0,5)]
    private float _speed = 1f;
    [SerializeField]
    private ParticleSystem _projectileParticles;

    private Transform _target;

    // Start is called before the first frame update
    private void Start()
    {
        _target = FindObjectOfType<Enemy>()?.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }
    private void FindClosestTarget()
    {
        var enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach(var enemy in enemies)
        {
            if (!enemy.isActiveAndEnabled)
                continue;
            var targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(targetDistance< maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        _target = closestTarget;
    }
    private void AimWeapon()
    {
        if (_target == null)
        {
            SetAttackingState(false);
            return;
        }
        _weapon.transform.LookAt(_target);
        var targetDistance = Vector3.Distance(transform.position, _target.position);
        if(targetDistance < _range)
        {
            SetAttackingState(true);
        }
        else
        {
            SetAttackingState(false);
        }
    }
    void SetAttackingState(bool isActive)
    {
        var emissionModule = _projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
