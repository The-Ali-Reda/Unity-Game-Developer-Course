using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    [SerializeField]
    private Vector3 _movementVector;
    [SerializeField]
    [Range(0,1)]
    private float _movementFactor;
    [SerializeField]
    private float period = 2f;

    private Vector3 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //number of cycles elapsed till now
        const float tau = 2 * Mathf.PI; //number of radians in a "cycle"
        float rawSinWave = Mathf.Sin(cycles * tau); //sin the number of radians passed
        _movementFactor = Mathf.Abs(rawSinWave); //0-1;so we can go from startPosition to (startPosition + offset) where offset is decided by the factor
        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startPosition + offset;
    }
}
