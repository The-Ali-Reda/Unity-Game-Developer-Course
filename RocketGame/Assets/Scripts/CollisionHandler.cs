using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private float delayInSeconds = 1.0f;
    [SerializeField]
    private AudioClip _crash;
    [SerializeField]
    private AudioClip _finish;
    [SerializeField]
    private ParticleSystem _successParticles;
    [SerializeField]
    private ParticleSystem _crashParticles;
    private AudioSource _audioSource;
    bool isTransitioning = false;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //no point in colliding after dying or passing
        if (isTransitioning)
            return;
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
        isTransitioning = true;
        _successParticles.Play();
        _audioSource.Stop();
        _audioSource.PlayOneShot(_finish);
        Invoke("LoadNextLevel", delayInSeconds);
    }
    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        isTransitioning = true;
        _crashParticles.Play();
        _audioSource.Stop();
        _audioSource.PlayOneShot(_crash);
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
