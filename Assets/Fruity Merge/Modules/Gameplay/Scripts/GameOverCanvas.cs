using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
    
    public void MainMenuNavigate()
    {
        SceneManager.LoadScene("Menu");
        GameManager.Instance.ChangeState(UIState.NONE);
    }

    public void Continue()
    {
        GameManager.Instance.ChangeState(UIState.NONE);
        GameManager.Instance.fruitSpawner.GetComponent<SpawnerController>().enabled = true;
        SceneManager.LoadScene("Game");
       
        //add implementation later

    }

}
