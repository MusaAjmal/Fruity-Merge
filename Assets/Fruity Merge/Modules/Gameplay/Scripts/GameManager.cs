using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using Voodoo.Utils;




public enum UIState
{
   NONE,
   OPEN_MAINMENU,
   CLOSE_OVERLAYS,
   OPEN_PAUSEMENU,
   OPEN_GAMEOVER,
   OPEN_SETTINGS,
   NAVIGATE_MAINMENU,
   PLAY_GAME
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool hapticToggle;
    public bool isPaused;

    [SerializeField] public GameObject fruitSpawner;
    public Action onStateChanged;

    public UIState uiState;

    

    private void Start()
    {
        isPaused = false;
        ChangeState(UIState.NONE);
        Vibrations.canVibrate = true;
    }

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
    public bool isGamePaused()
    {
        return isPaused;
    }
    public void ManageHaptic()
    {
        //hapticToggle = !hapticToggle;
        Vibrations.canVibrate = !Vibrations.canVibrate;

        PlayerPrefs.SetInt("HapticToggled", Vibrations.canVibrate ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsHapticToggled()
    {
        return Vibrations.canVibrate;
    }

    public void PlayButtonClickSound()
    {
        SoundManager.instance.PlayOneShotSound("Button_Click");
    }

    public void PauseGame()
    {
        SetPause(!isPaused);
    }

    public void SetPause(bool value)
    {
        isPaused = value;


        Time.timeScale = isPaused ? 0f : 1f;
        if (isPaused)
            ChangeState(UIState.OPEN_PAUSEMENU);
        else
            ChangeState(UIState.CLOSE_OVERLAYS);
        
    }

    public void ChangeState(UIState state)
    {
        SoundManager.instance.PlayOneShotSound("Button_Click");
        uiState = state;
        onStateChanged?.Invoke();
    }


    public void StopSpawning()
    {
        //fruitSpawner.SetActive(false);
        fruitSpawner.GetComponent<SpawnerController>().enabled = false;
        ChangeState(UIState.OPEN_GAMEOVER);
    }

}