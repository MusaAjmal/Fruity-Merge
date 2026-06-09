using UnityEngine;
using System.Collections;
public class Peak : MonoBehaviour
{
    private Coroutine gameOverRoutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Fruit fruit = other.GetComponent<Fruit>();

        if (fruit != null)
        {
            // Start 2-second check when fruit enters
            gameOverRoutine = StartCoroutine(GameOverTimer(fruit));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Fruit fruit = other.GetComponent<Fruit>();

        if (fruit != null)
        {
            // Cancel timer if fruit leaves before 2 seconds
            if (gameOverRoutine != null)
            {
                StopCoroutine(gameOverRoutine);
                gameOverRoutine = null;
            }
        }
    }

    private IEnumerator GameOverTimer(Fruit fruit)
    {
        yield return new WaitForSeconds(2f);

        if (fruit != null)
        {
            Debug.Log("GAME OVER");
        }

        gameOverRoutine = null;
    }
}
