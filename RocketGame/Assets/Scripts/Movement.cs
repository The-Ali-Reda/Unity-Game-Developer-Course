using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    [SerializeField]
    private float _thrustPower = 100f;
    [SerializeField]
    private float _rotationThrust = 1f;
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
            _rigidbody.AddRelativeForce(Vector3.up* _thrustPower * Time.deltaTime, ForceMode.Impulse);
            if(!_audioSource.isPlaying)
                _audioSource.Play();

        }
        else
        {
            _audioSource.Stop();
        }
    }
    void ProcessRotation()
    {
        var leftPressed = Input.GetKey(KeyCode.A);
        var rightPressed = Input.GetKey(KeyCode.D);
        if (leftPressed)
        {
            ApplyRotation(_rotationThrust);
        }
        else if (rightPressed)
        {
            
            ApplyRotation(-_rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; //freezes rotation so we can manually rotate
        transform.Rotate(Vector3.forward, rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false; //freezes rotation so we can manually rotate
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }
}
