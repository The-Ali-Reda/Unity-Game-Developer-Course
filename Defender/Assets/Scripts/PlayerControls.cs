using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlayerControls : MonoBehaviour
{
    #region Serialized Fields
    [Header("General Setup Settings")]
    [Tooltip("How fast the player ship moves")]
    [SerializeField]
    private float _controlSpeed = 30f;
    [SerializeField]
    [Tooltip("How far the player can move along x-axis from center")]
    private float xRange = 10f;
    [SerializeField]
    [Tooltip("How far the player can move along y-axis from center")]
    private float yRange = 5f;

    [Header("Screen Based Tuning")]
    [SerializeField]
    private float _positionPitchFactor = -2f;
    [SerializeField]
    private float _positionYawFactor = -2f;

    [Header("Player Control Tuning")]
    [SerializeField]
    private float _controlPitchFactor = -10f;
    [SerializeField]
    private float _controlRollFactor = -10f;
    [SerializeField]
    private float _rotationFactor = 1f;
    [SerializeField]
    [Header("Laser gun array")]
    [Tooltip("Add all guns here")]
    private GameObject[] _lasers;
    #endregion

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
            SetLasersActive(true);
        } else
        {
            SetLasersActive(false);
        }
    }
    private void SetLasersActive(bool active)
    {
        foreach (var laser in _lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = active;
        }
    }
    
}
