using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] 
    private Transform _weapon;
    
    [SerializeField]
    [Range(0,5)]
    private float _speed = 1f;


    private Transform _target;

    // Start is called before the first frame update
    private void Start()
    {
        _target = FindObjectOfType<EnemyMover>()?.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        _weapon.transform.LookAt(_target);
        //StartCoroutine(AimWeapon());
    }
    private IEnumerator AimWeapon()
    {
        var enemyPosition = _target.position;
        var startRotation = _weapon.rotation;
        var direction = enemyPosition - transform.position;
        var endRotation = Quaternion.LookRotation(direction);
        var lerpPercent = 0f;
        while (lerpPercent < 1)
        {
            lerpPercent += Time.deltaTime * _speed;
            _weapon.rotation = Quaternion.Lerp(startRotation, endRotation, lerpPercent);
            yield return null;
        }
    }
}
