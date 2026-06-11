using UnityEngine;

public class MainGameCanvas : MonoBehaviour
{

    private static int highScore;
    private int currentScore;
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void OpenSettings()
    {
        GameManager.Instance.ChangeState(UIState.OPEN_SETTINGS);
    }

    public void OpenPauseMenu()
    {
        GameManager.Instance.ChangeState(UIState.OPEN_PAUSEMENU);
    }

    public void UpdateScore()
    {

    }

    public void UpdateHighScore()
    {
        if( currentScore > highScore)
        {
            highScore = currentScore;
        }
        else
        {
            return;
        }
    }

    private void UpdateHighScoreUI()
    {

    }
    public void UpdateScoreUI()
    {

    }

}
