//using UnityEngine;

//public class MergeManager : MonoBehaviour
//{
//    public static MergeManager Instance { get; private set; }
//    [Header("Database")]
//    [SerializeField] private FruitDatabase fruitDatabase;
//    [Header("Visual Effects")]
//    [SerializeField] private GameObject mergeEffectPrefab; // Optional visual effect prefab
//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }
//    /// <summary>
//    /// Merges two fruits, spawning the next tier from the Database and destroying the originals.
//    /// </summary>
//    public void MergeFruits(Fruit fruitA, Fruit fruitB)
//    {
//        if (fruitDatabase == null)
//        {
//            Debug.LogError("FruitDatabase is not assigned to the MergeManager!");
//            return;
//        }
//        int currentId = fruitA.Data.FruitId;
//        FruitSO nextFruitData = fruitDatabase.GetNextFruit(currentId);
//        // Find the midpoint of the collision
//        Vector3 spawnPosition = (fruitA.transform.position + fruitB.transform.position) / 2f;
//        // Spawn next tier fruit
//        if (nextFruitData != null && nextFruitData.Prefab != null)
//        {
//            GameObject newFruit = Instantiate(nextFruitData.Prefab, spawnPosition, Quaternion.identity);
//            // Add a slight upward pop force to make the merge feel dynamic
//            Rigidbody2D rb = newFruit.GetComponent<Rigidbody2D>();
//            if (rb != null)
//            {
//                rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
//            }
//        }
//        else
//        {
//            // Handled when reaching the absolute largest fruit (e.g., Watermelon + Watermelon)
//            Debug.Log("Max tier fruit merged and cleared!");
//        }
//        // Spawn optional particle effects
//        if (mergeEffectPrefab != null)
//        {
//            Instantiate(mergeEffectPrefab, spawnPosition, Quaternion.identity);
//        }
//        // Clean up the two old fruit objects
//        Destroy(fruitA.gameObject);
//        Destroy(fruitB.gameObject);
//    }
//}
//using UnityEngine;

//public class MergeManager : MonoBehaviour
//{
//    public static MergeManager Instance { get; private set; }

//    [Header("Database")]
//    [SerializeField] private FruitDatabase fruitDatabase;

//    [Header("Visual Effects")]
//    [SerializeField] private GameObject mergeEffectPrefab;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    public void MergeFruits(Fruit fruitA, Fruit fruitB)
//    {
//        if (fruitDatabase == null)
//        {
//            Debug.LogError("FruitDatabase is not assigned to the MergeManager!");
//            return;
//        }

//        int currentId = fruitA.Data.FruitId;
//        FruitSO nextFruitData = fruitDatabase.GetNextFruit(currentId);

//        // Find the midpoint of the collision
//        Vector3 spawnPosition = (fruitA.transform.position + fruitB.transform.position) / 2f;

//        // Spawn next tier fruit
//        if (nextFruitData != null && nextFruitData.Prefab != null)
//        {
//            GameObject newFruit = Instantiate(nextFruitData.Prefab, spawnPosition, Quaternion.identity);

//            // Apply scale immediately to prevent a 1-frame visual flicker
//            Fruit fruitComponent = newFruit.GetComponent<Fruit>();
//            if (fruitComponent != null)
//            {
//                newFruit.transform.localScale = fruitComponent.TargetScale;
//            }

//            // Add a slight upward pop force to make the merge feel dynamic
//            Rigidbody2D rb = newFruit.GetComponent<Rigidbody2D>();
//            if (rb != null)
//            {
//                rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
//            }
//        }
//        else
//        {
//            Debug.Log("Max tier fruit merged and cleared!");
//        }

//        // Spawn visual effect
//        if (mergeEffectPrefab != null)
//        {
//            Instantiate(mergeEffectPrefab, spawnPosition, Quaternion.identity);
//        }

//        // Clean up the two old fruit objects
//        Destroy(fruitA.gameObject);
//        Destroy(fruitB.gameObject);
//    }
//}
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance { get; private set; }

    [Header("Database")]
    [SerializeField] private FruitDatabase fruitDatabase;

    [Header("Visual Effects")]
    [SerializeField] private GameObject mergeEffectPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MergeFruits(Fruit fruitA, Fruit fruitB)
    {
        if (fruitDatabase == null)
        {
            Debug.LogError("FruitDatabase is not assigned to the MergeManager!");
            return;
        }

        int currentId = fruitA.Data.FruitId;
        FruitSO nextFruitData = fruitDatabase.GetNextFruit(currentId);

        // Find the midpoint of the collision
        Vector3 spawnPosition = (fruitA.transform.position + fruitB.transform.position) / 2f;

        // Spawn next tier fruit
        if (nextFruitData != null && nextFruitData.Prefab != null)
        {
            GameObject newFruit = Instantiate(nextFruitData.Prefab, spawnPosition, Quaternion.identity);

            // Apply scale immediately to prevent a 1-frame visual flicker
            Fruit fruitComponent = newFruit.GetComponent<Fruit>();
            if (fruitComponent != null)
            {
                newFruit.transform.localScale = fruitComponent.TargetScale;
            }

            // Add a slight upward pop force to make the merge feel dynamic
            Rigidbody2D rb = newFruit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
            }
        }
        else
        {
            // Triggered when merging two of the largest fruits
            Debug.Log("Max tier fruit merged and cleared!");
        }

        // Spawn visual effect
        if (mergeEffectPrefab != null)
        {
            Instantiate(mergeEffectPrefab, spawnPosition, Quaternion.identity);
        }

        // Clean up the two old fruit objects
        Destroy(fruitA.gameObject);
        Destroy(fruitB.gameObject);
    }
}