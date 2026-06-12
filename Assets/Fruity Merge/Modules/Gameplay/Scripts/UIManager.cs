using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public static UIManager Instance { get; private set; }

    //[Header("Scene Canvases")]
    //[SerializeField] private GameObject loadingScreenCanvas;
    //[SerializeField] private GameObject mainMenuCanvas;
    //[SerializeField] private GameObject mainLevelCanvas;

    //[Header("Overlay Canvases")]
    //[SerializeField] private GameObject settingsCanvas;
    //[SerializeField] private GameObject gameOverCanvas;
    //[SerializeField] private GameObject pauseMenuCanvas;


    //private GameObject currentSceneCanvas;
    //private GameObject currentOverlayCanvas;



    //private void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    Instance = this;
    //    DontDestroyOnLoad(gameObject);
    //}

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //    //if (GameManager.Instance != null)
    //    //    GameManager.Instance.onStateChanged += UpdateCanvas;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //    //if (GameManager.Instance != null)
    //    //    GameManager.Instance.onStateChanged -= UpdateCanvas;
    //}


    //private void Start()
    //{
    //    StartCoroutine(InitWhenReady());
    //}

    //private IEnumerator InitWhenReady()
    //{
    //    while (GameManager.Instance == null)
    //        yield return null;

    //    GameManager.Instance.onStateChanged += UpdateCanvas;

    //    UpdateCanvas(); // IMPORTANT: sync UI immediately
    //}







    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (currentSceneCanvas != null)
    //    {
    //        Destroy(currentSceneCanvas);
    //        currentSceneCanvas = null;
    //    }

    //    switch (scene.buildIndex)
    //    {
    //        case 0:
    //            currentSceneCanvas = Instantiate(loadingScreenCanvas);
    //            break;

    //        case 1:
    //            currentSceneCanvas = Instantiate(mainMenuCanvas);
    //            break;

    //        case 2:
    //            currentSceneCanvas = Instantiate(mainLevelCanvas);
    //            break;
    //    }
    //}

    //// ---------------- OVERLAYS ----------------

    //private void UpdateCanvas()
    //{
    //    UIState state = GameManager.Instance.uiState;

    //    switch (state)
    //    {
    //        case UIState.OPEN_MAINMENU:
    //            CloseOverlay();
    //            break;

    //        case UIState.OPEN_SETTINGS:
    //            OpenSettings();
    //            break;

    //        case UIState.OPEN_GAMEOVER:
    //            OpenGameOver();
    //            break;

    //        case UIState.OPEN_PAUSEMENU:
    //            OpenPauseMenu();
    //            break;

    //        case UIState.CLOSE_OVERLAYS:
    //            CloseOverlay();
    //            break;

    //        case UIState.PLAY_GAME:
    //            PlayGame();
    //            break;

    //        case UIState.NAVIGATE_MAINMENU:
    //            NavigateMainMenu();
    //            break;
    //    }
    //}

    //public void OpenSettings()
    //{
    //    if (currentOverlayCanvas != null)
    //        Destroy(currentOverlayCanvas);

    //    currentOverlayCanvas = Instantiate(settingsCanvas);
    //}

    //public void OpenGameOver()
    //{
    //    if (currentOverlayCanvas != null)
    //        Destroy(currentOverlayCanvas);

    //    currentOverlayCanvas = Instantiate(gameOverCanvas);
    //}

    //public void OpenPauseMenu()
    //{
    //    if (currentOverlayCanvas != null)
    //        Destroy(currentOverlayCanvas);

    //    currentOverlayCanvas = Instantiate(pauseMenuCanvas);
    //}

    //public void CloseOverlay()
    //{
    //    if (currentOverlayCanvas != null)
    //    {
    //        Destroy(currentOverlayCanvas);
    //        currentOverlayCanvas = null;
    //    }
    //}

    //public void PlayGame()
    //{
    //    SceneManager.LoadScene("Game");
    //    Debug.Log("scene loaded with name: "+ SceneManager.GetActiveScene().name);
    //}

    //public void NavigateMainMenu()
    //{
    //    SceneManager.LoadScene("Menu");
    //}

    public static UIManager Instance { get; private set; }

    [Header("Scene Canvases")]
    [SerializeField] private GameObject loadingScreenCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject mainLevelCanvas;

    [Header("Overlay Canvases")]
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;

    [Header("Animation Settings")]
    [SerializeField] private float panelFadeDuration = 0.3f;
    [SerializeField] private float panelScaleDuration = 0.3f;
    [SerializeField] private float closeDelay = 0.25f; // wait for anim before Destroy

    private GameObject currentSceneCanvas;
    private GameObject currentOverlayCanvas;

    

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void Start() => StartCoroutine(InitWhenReady());

    private IEnumerator InitWhenReady()
    {
        while (GameManager.Instance == null) yield return null;
        GameManager.Instance.onStateChanged += UpdateCanvas;
        UpdateCanvas();
    }

    

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (currentSceneCanvas != null)
        {
            Destroy(currentSceneCanvas);
            currentSceneCanvas = null;
        }

        GameObject prefab = scene.buildIndex switch
        {
            0 => loadingScreenCanvas,
            1 => mainMenuCanvas,
            2 => mainLevelCanvas,
            _ => null
        };

        if (prefab != null)
        {
            currentSceneCanvas = Instantiate(prefab);
            AnimateIn(currentSceneCanvas);
            Inject(currentSceneCanvas);
        }
    }

    

    private void UpdateCanvas()
    {
        switch (GameManager.Instance.uiState)
        {
            case UIState.OPEN_MAINMENU: CloseOverlay(); break;
            case UIState.OPEN_SETTINGS: OpenSettings(); break;
            case UIState.OPEN_GAMEOVER: OpenGameOver(); break;
            case UIState.OPEN_PAUSEMENU: OpenPauseMenu(); break;
            case UIState.CLOSE_OVERLAYS: CloseOverlay(); break;
            case UIState.PLAY_GAME: PlayGame(); break;
            case UIState.NAVIGATE_MAINMENU: NavigateMainMenu(); break;
        }
    }

    

    public void OpenSettings() => OpenOverlay(settingsCanvas);
    public void OpenGameOver() => OpenOverlay(gameOverCanvas);
    public void OpenPauseMenu() => OpenOverlay(pauseMenuCanvas);

    private void OpenOverlay(GameObject prefab)
    {
        // Close existing overlay first (instant, no double-anim)
        if (currentOverlayCanvas != null)
        {
            Destroy(currentOverlayCanvas);
            currentOverlayCanvas = null;
        }

        currentOverlayCanvas = Instantiate(prefab);
        AnimateIn(currentOverlayCanvas);
        Inject(currentOverlayCanvas);
    }

    public void CloseOverlay()
    {
        if (currentOverlayCanvas == null) return;

        GameObject toClose = currentOverlayCanvas;
        currentOverlayCanvas = null;           // clear ref immediately
        AnimateOut(toClose, () => Destroy(toClose));
    }

   

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Scene loaded: " + SceneManager.GetActiveScene().name);
    }

    public void NavigateMainMenu() => SceneManager.LoadScene("Menu");

   

    
    private void AnimateIn(GameObject panel)
    {
        if (panel == null) return;

       
        CanvasGroup cg = GetOrAddCanvasGroup(panel);
        cg.alpha = 0f;
        LeanTween.alphaCanvas(cg, 1f, panelFadeDuration)
                 .setEase(LeanTweenType.easeOutCubic);

        
        panel.transform.localScale = Vector3.one * 0.85f;
        LeanTween.scale(panel, Vector3.one, panelScaleDuration)
                 .setEase(LeanTweenType.easeOutBack);
    }
    private void AnimateOut(GameObject panel, Action onComplete = null)
    {
        if (panel == null) { onComplete?.Invoke(); return; }

        CanvasGroup cg = GetOrAddCanvasGroup(panel);

        LeanTween.alphaCanvas(cg, 0f, closeDelay)
                 .setEase(LeanTweenType.easeInCubic);

        LeanTween.scale(panel, Vector3.one * 0.85f, closeDelay)
                 .setEase(LeanTweenType.easeInBack)
                 .setOnComplete(() => onComplete?.Invoke());
    }

    private CanvasGroup GetOrAddCanvasGroup(GameObject go)
    {
        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        if (cg == null) cg = go.AddComponent<CanvasGroup>();
        return cg;
    }

    private void Inject(GameObject root)
    {
        if (root == null) return;

        foreach (Button btn in root.GetComponentsInChildren<Button>(true))
        {
            if (btn == null) continue;
            if (btn.GetComponent<ButtonClickAnim>() == null)
                btn.gameObject.AddComponent<ButtonClickAnim>();
        }
    }

}


