using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private float _levelLoadDelay = 1f;
    [SerializeField]
    private ParticleSystem _explosionVFX;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} triggered: {other.gameObject.name}");
        StartCrashSequence();

    }
    private void StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;
        _explosionVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadLevel", _levelLoadDelay);
    }
    private void ReloadLevel()
    {
        var idx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idx);
    }
}
