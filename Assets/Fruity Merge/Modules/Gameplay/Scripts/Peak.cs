using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Peak : MonoBehaviour
{
    //private Coroutine gameOverRoutine;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log("game over initiated");
    //    Fruit fruit = other.GetComponent<Fruit>();

    //    if (fruit != null)
    //    {
    //        // Start 2-second check when fruit enters

    //        //gameOverRoutine = StartCoroutine(GameOverTimer(fruit));
    //    }
    //}


    ////private void OnTriggerStay2D(Collider2D collision)
    ////{
    ////    Debug.Log("HI MOM!");
    ////}
    ////private void OnTriggerExit2D(Collider2D other)
    ////{
    ////    Fruit fruit = other.GetComponent<Fruit>();

    ////    if (fruit != null)
    ////    {
    ////        // Cancel timer if fruit leaves before 2 seconds
    ////        if (gameOverRoutine != null)
    ////        {
    ////            StopCoroutine(gameOverRoutine);
    ////            gameOverRoutine = null;
    ////        }
    ////    }
    ////}

    //private IEnumerator GameOverTimer(Fruit fruit)
    //{
    //    yield return new WaitForSeconds(2f);

    //    if (fruit != null)
    //    {
    //        Debug.Log("GAME OVER");
    //    }

    //    gameOverRoutine = null;
    //}

    private HashSet<Collider2D> collidersInside = new HashSet<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Happened with: " + collision.gameObject.name);

        collidersInside.Add(collision);

        StartCoroutine(CheckForGameOver(collision));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Object Left Trigger: " + collision.gameObject.name);

        collidersInside.Remove(collision);
    }

    private IEnumerator CheckForGameOver(Collider2D collision)
    {
        yield return new WaitForSeconds(2f);

        if (collision != null && collidersInside.Contains(collision))
        {
            Debug.Log("GAME OVER");
            GameManager.Instance.StopSpawning();
        }
    }



}
