using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
   public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    public void SettingsIcon()
    {
        //GameManager.Instance.enableCanvas("Settings");
        GameManager.Instance.ChangeState(UIState.OPEN_SETTINGS);
       // UIManager.Instance.OpenSettings();
       // GameManager.Instance.ChangeUIState(UIState.MAIN_MENU);
    }
}
