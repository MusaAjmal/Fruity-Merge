using System;
using System.Collections.Generic;
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


    public Action onStateChanged;

    public UIState uiState;

    

    private void Start()
    {
        isPaused = false;
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
        hapticToggle = !hapticToggle;
        Vibrations.canVibrate = hapticToggle;

        PlayerPrefs.SetInt("HapticToggled", hapticToggle ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsHapticToggled()
    {
        return hapticToggle;
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
        uiState = state;
        onStateChanged?.Invoke();
    }




}