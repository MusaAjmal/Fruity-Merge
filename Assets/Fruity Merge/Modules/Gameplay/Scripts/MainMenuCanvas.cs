using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject vfx;
   public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //StartCoroutine(LoadGameWithVFX());
    }

    private IEnumerator LoadGameWithVFX()
    {
        // 1. Spawn VFX
        GameObject fx = null;

        if (vfx != null)
        {
            fx = Instantiate(vfx,transform);
        }

        // 2. Wait for effect duration
        yield return new WaitForSeconds(1.5f);

        // 3. Destroy VFX
        if (fx != null)
        {
            Destroy(fx);
        }

        // 4. Load scene AFTER VFX
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
