using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonAnimInjector : MonoBehaviour
{
    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void Start() => InjectAll();

    private void OnSceneLoaded(Scene s, LoadSceneMode m) => InjectAll();

    public void InjectAll()
    {
        foreach (Button btn in FindObjectsByType<Button>(FindObjectsSortMode.None))
        {
            if (btn.GetComponent<ButtonClickAnim>() == null)
                btn.gameObject.AddComponent<ButtonClickAnim>();
        }
    }
}
