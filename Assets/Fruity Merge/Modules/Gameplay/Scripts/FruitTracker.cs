using UnityEngine;

/// <summary>
/// Added dynamically to spawned fruits. Tracks birth time and landed status.
/// </summary>
public class FruitTracker : MonoBehaviour
{
    [System.NonSerialized] public float spawnTime;
    [System.NonSerialized] public bool hasLanded = false;

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore trigger colliders (like the limit line itself)
        if (collision.collider.isTrigger) return;

        // The fruit has landed if it collides with the floor, walls, or another fruit.
        hasLanded = true;
    }
}