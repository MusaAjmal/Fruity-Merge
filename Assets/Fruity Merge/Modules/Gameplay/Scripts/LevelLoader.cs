using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float minimumLoadTime = 5f;
    //[SerializeField] private TextMeshProUGUI loadedAmount;

    private void Start()
    {
        LoadLevel(1);
    }

    public void LoadLevel(int sceneIndex)
    {
       StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private IEnumerator LoadAsynchronously(int index)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;

        float elapsed = 0f;

        while (elapsed < minimumLoadTime || operation.progress < 0.9f)
        {
            elapsed += Time.deltaTime;
            float loadProgress = Mathf.Clamp01(operation.progress / 0.9f);
            float timeProgress = Mathf.Clamp01(elapsed / minimumLoadTime);

            // Slider reflects both time and load progress
            slider.value = Mathf.Min(loadProgress, timeProgress);
            //loadedAmount.text = Mathf.RoundToInt(slider.value * 100f) + "%";

            yield return null;
        }

        slider.value = 1f;
        yield return new WaitForSeconds(0.1f); // let slider visually complete

        operation.allowSceneActivation = true;

    }
}
