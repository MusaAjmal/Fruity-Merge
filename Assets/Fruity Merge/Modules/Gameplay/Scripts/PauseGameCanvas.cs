using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class PauseGameCanvas : MonoBehaviour
{
   //close, resume, restart, home



    public void ClosePauseMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ChangeState(UIState.CLOSE_OVERLAYS);
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ChangeState(UIState.CLOSE_OVERLAYS);
    }

    public void RestartGame()
    {
        GameManager.Instance.ChangeState(UIState.PLAY_GAME);
    }

    public void MainMenuGame()
    {
        GameManager.Instance.ChangeState(UIState.NAVIGATE_MAINMENU);
    }
}
