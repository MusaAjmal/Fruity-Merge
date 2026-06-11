using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Scene Canvases")]
    [SerializeField] private GameObject loadingScreenCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject mainLevelCanvas;

    [Header("Overlay Canvases")]
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;
    

    private GameObject currentSceneCanvas;
    private GameObject currentOverlayCanvas;

    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //if (GameManager.Instance != null)
        //    GameManager.Instance.onStateChanged += UpdateCanvas;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //if (GameManager.Instance != null)
        //    GameManager.Instance.onStateChanged -= UpdateCanvas;
    }


    private void Start()
    {
        StartCoroutine(InitWhenReady());
    }

    private IEnumerator InitWhenReady()
    {
        while (GameManager.Instance == null)
            yield return null;

        GameManager.Instance.onStateChanged += UpdateCanvas;

        UpdateCanvas(); // IMPORTANT: sync UI immediately
    }







    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (currentSceneCanvas != null)
        {
            Destroy(currentSceneCanvas);
            currentSceneCanvas = null;
        }

        switch (scene.buildIndex)
        {
            case 0:
                currentSceneCanvas = Instantiate(loadingScreenCanvas);
                break;

            case 1:
                currentSceneCanvas = Instantiate(mainMenuCanvas);
                break;

            case 2:
                currentSceneCanvas = Instantiate(mainLevelCanvas);
                break;
        }
    }

    // ---------------- OVERLAYS ----------------

    private void UpdateCanvas()
    {
        UIState state = GameManager.Instance.uiState;

        switch (state)
        {
            case UIState.OPEN_MAINMENU:
                CloseOverlay();
                break;

            case UIState.OPEN_SETTINGS:
                OpenSettings();
                break;

            case UIState.OPEN_GAMEOVER:
                OpenGameOver();
                break;

            case UIState.OPEN_PAUSEMENU:
                OpenPauseMenu();
                break;

            case UIState.CLOSE_OVERLAYS:
                CloseOverlay();
                break;

            case UIState.PLAY_GAME:
                CloseOverlay();
                break;

            case UIState.NAVIGATE_MAINMENU:
                NavigateMainMenu();
                break;
        }
    }

    public void OpenSettings()
    {
        if (currentOverlayCanvas != null)
            Destroy(currentOverlayCanvas);

        currentOverlayCanvas = Instantiate(settingsCanvas);
    }

    public void OpenGameOver()
    {
        if (currentOverlayCanvas != null)
            Destroy(currentOverlayCanvas);

        currentOverlayCanvas = Instantiate(gameOverCanvas);
    }

    public void OpenPauseMenu()
    {
        if (currentOverlayCanvas != null)
            Destroy(currentOverlayCanvas);

        currentOverlayCanvas = Instantiate(pauseMenuCanvas);
    }

    public void CloseOverlay()
    {
        if (currentOverlayCanvas != null)
        {
            Destroy(currentOverlayCanvas);
            currentOverlayCanvas = null;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void NavigateMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}


