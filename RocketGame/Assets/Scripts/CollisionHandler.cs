using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private float delayInSeconds = 1.0f;
    private void OnCollisionEnter(Collision collision)
    {
        //other object
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with a Friendly object");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Obstacle":
            default:
                StartCrashSequence();
                break;
        }
    }
    private void StartSuccessSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayInSeconds);
    }
    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayInSeconds);
    }
    private void ReloadLevel()
    {
        var levelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
    private void LoadNextLevel()
    {
        var levelIndex = SceneManager.GetActiveScene().buildIndex;
        var nextLevelIndex = (levelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        Debug.Log(nextLevelIndex);
        SceneManager.LoadScene(nextLevelIndex, LoadSceneMode.Single);
    }
}
