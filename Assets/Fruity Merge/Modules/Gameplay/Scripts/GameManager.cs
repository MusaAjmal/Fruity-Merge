using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Scene currentScene;
    [SerializeField] private GameObject loadingScreenCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
 [SerializeField]   private GameObject gameOverInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex == 0)
        {
            Debug.Log("Loading Screen Scene !!!");
            Instantiate(loadingScreenCanvas);
        }

        if (currentScene.buildIndex == 1)
        {
            Instantiate(mainMenuCanvas);
        }
    }

    
    

}