using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheat : MonoBehaviour
{

    private CollisionHandler _collisionHandler;
    // Start is called before the first frame update
    void Start()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCollisions();
        }
    }
    private void LoadNextLevel()
    {
        var levelIndex = SceneManager.GetActiveScene().buildIndex;
        var nextLevelIndex = (levelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex, LoadSceneMode.Single);
    }
    private void ToggleCollisions()
    {
        _collisionHandler.isCollisionActive = !_collisionHandler.isCollisionActive;
    }
}
