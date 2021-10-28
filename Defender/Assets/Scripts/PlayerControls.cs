using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private float _controlSpeed = 30f;
    [SerializeField]
    private float xRange = 10f;
    [SerializeField]
    private float yRange = 5f;
    [SerializeField]
    private float _positionPitchFactor = -2f;
    [SerializeField]
    private float _controlPitchFactor = -10f;
    [SerializeField]
    private float _positionYawFactor = -2f;
    [SerializeField]
    private float _controlRollFactor = -10f;
    [SerializeField]
    private float _rotationFactor = 1f;
    
    private float _xThrow;
    private float _yThrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * _positionPitchFactor;
        float pitchDueToControl = _yThrow * _controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * _positionYawFactor;
        float roll = _xThrow * -_controlRollFactor;

        var targetRotation = Quaternion.Euler(pitch, yaw, roll);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, _rotationFactor);
    }

    private void ProcessTranslation()
    {
        _xThrow = Input.GetAxis("Horizontal");
        _yThrow = Input.GetAxis("Vertical");
        float xOffset = _xThrow * Time.deltaTime * _controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float yOffset = _yThrow * Time.deltaTime * _controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;

        float newXPosClamped = Mathf.Clamp(rawXPos, -xRange, xRange);
        float newYPosClamped = Mathf.Clamp(rawYPos, -yRange, yRange);
        transform.localPosition = new Vector3(newXPosClamped, newYPosClamped, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {

        }
    }
}
