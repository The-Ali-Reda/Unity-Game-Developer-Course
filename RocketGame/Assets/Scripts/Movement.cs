using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _thrustPower = 100f;
    [SerializeField]
    private float _rotationThrust = 1f;
    [SerializeField]
    private AudioClip _mainEngine;
    [SerializeField]
    private ParticleSystem _rocketThrust;
    [SerializeField]
    private ParticleSystem _rightThrust;
    [SerializeField]
    private ParticleSystem _leftThrust;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    
    void ProcessThrust()
    {
        var spacePressed = Input.GetKey(KeyCode.Space);
        
        if (spacePressed)
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        var leftPressed = Input.GetKey(KeyCode.A);
        var rightPressed = Input.GetKey(KeyCode.D);
        if (leftPressed)
        {
            RotateLeft();
        }
        else if (rightPressed)
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }
    private void StopThrusting()
    {
        _audioSource.Stop();
        _rocketThrust.Stop();
    }

    private void StartThrusting()
    {
        _rigidbody.AddRelativeForce(Vector3.up * _thrustPower * Time.deltaTime, ForceMode.Impulse);
        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(_mainEngine);
        if (_rocketThrust.isStopped)
            _rocketThrust.Play();
    }

    private void StopRotating()
    {
        _rightThrust.Stop();
        _leftThrust.Stop();
    }

    private void RotateRight()
    {
        _rightThrust.Stop();
        if (_leftThrust.isStopped)
            _leftThrust.Play();
        ApplyRotation(-_rotationThrust);
    }

    private void RotateLeft()
    {
        _leftThrust.Stop();
        if (_rightThrust.isStopped)
            _rightThrust.Play();
        ApplyRotation(_rotationThrust);
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; //freezes rotation so we can manually rotate
        transform.Rotate(Vector3.forward, rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false; //freezes rotation so we can manually rotate
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }
}
