//using UnityEngine;

//[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(Rigidbody2D))]
//public class Fruit : MonoBehaviour
//{
//    [Header("Fruit Reference")]
//    [SerializeField] private FruitSO fruitData;
//    public FruitSO Data => fruitData;

//    private SpriteRenderer fruitSprite;
//    private bool isMerged = false; // Flag to prevent double merging in the same frame

//    private void Start()
//    {
//        fruitSprite = GetComponent<SpriteRenderer>();
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
//        if (otherFruit == null) return;

//        // Ensure both fruits have valid ScriptableObject data and match IDs
//        if (fruitData != null && otherFruit.Data != null && fruitData.FruitId == otherFruit.Data.FruitId)
//        {
//            // Stop if either fruit has already initiated a merge
//            if (isMerged || otherFruit.isMerged) return;

//            // Mark both as merged immediately
//            isMerged = true;
//            otherFruit.isMerged = true;

//            // Notify MergeManager
//            if (MergeManager.Instance != null)
//            {
//                MergeManager.Instance.MergeFruits(this, otherFruit);
//            }
//            else
//            {
//                Debug.LogError("MergeManager Instance is missing in the scene!");
//            }
//        }
//    }
//}using UnityEngine;

//using UnityEngine;

//[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(Rigidbody2D))]
//public class Fruit : MonoBehaviour
//{
//    [Header("Fruit Reference")]
//    [SerializeField] private FruitSO fruitData;
//    public FruitSO Data => fruitData;

//    private SpriteRenderer fruitSprite;
//    private bool isMerged = false;

//    // Property to calculate target scale: Size 1.0 for ID 1, 1.2 for ID 2, 1.4 for ID 3, etc.
//    public Vector3 TargetScale
//    {
//        get
//        {
//            if (fruitData == null) return Vector3.one;
//            float scaleValue = 1.0f + (fruitData.FruitId - 1) * 0.2f;
//            return new Vector3(scaleValue, scaleValue, 1f);
//        }
//    }

//    private void Start()
//    {
//        fruitSprite = GetComponent<SpriteRenderer>();
//        // Automatically apply the calculated scale
//        transform.localScale = TargetScale;
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
//        if (otherFruit == null) return;

//        if (fruitData != null && otherFruit.Data != null && fruitData.FruitId == otherFruit.Data.FruitId)
//        {
//            if (isMerged || otherFruit.isMerged) return;

//            isMerged = true;
//            otherFruit.isMerged = true;

//            if (MergeManager.Instance != null)
//            {
//                MergeManager.Instance.MergeFruits(this, otherFruit);
//            }
//            else
//            {
//                Debug.LogError("MergeManager Instance is missing in the scene!");
//            }
//        }
//    }
//}
//using UnityEngine;

//[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(Rigidbody2D))]
//public class Fruit : MonoBehaviour
//{
//    [Header("Fruit Reference")]
//    [SerializeField] private FruitSO fruitData;
//    public FruitSO Data => fruitData;

//    [Header("Exponential Scale Settings")]
//    [Tooltip("The base scale of the smallest fruit (ID 1).")]
//    [SerializeField] private float baseScale = 1.0f;
//    [Tooltip("The multiplier applied for each subsequent ID. (e.g. 1.2 means each fruit is 20% larger than the previous one)")]
//    [SerializeField] private float growthFactor = 1.1f;

//    private SpriteRenderer fruitSprite;
//    private bool isMerged = false;

//    // Property to calculate exponential scale: baseScale * (growthFactor ^ (FruitId - 1))
//    public Vector3 TargetScale
//    {
//        get
//        {
//            if (fruitData == null) return Vector3.one;

//            // Formula: scale = base * (factor ^ (ID - 1))
//            float scaleValue = baseScale * Mathf.Pow(growthFactor, fruitData.FruitId - 1);
//            return new Vector3(scaleValue, scaleValue, 1f);
//        }
//    }

//    private void Start()
//    {
//        fruitSprite = GetComponent<SpriteRenderer>();
//        // Automatically apply the calculated exponential scale
//        transform.localScale = TargetScale;
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
//        if (otherFruit == null) return;

//        if (fruitData != null && otherFruit.Data != null && fruitData.FruitId == otherFruit.Data.FruitId)
//        {
//            if (isMerged || otherFruit.isMerged) return;

//            isMerged = true;
//            otherFruit.isMerged = true;

//            if (MergeManager.Instance != null)
//            {
//                MergeManager.Instance.MergeFruits(this, otherFruit);
//            }
//            else
//            {
//                Debug.LogError("MergeManager Instance is missing in the scene!");
//            }
//        }
//    }
//}
using Voodoo.Utils;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Fruit : MonoBehaviour
{
    [Header("Fruit Reference")]
    [SerializeField] private FruitSO fruitData;
    public FruitSO Data => fruitData;
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private Sprite blinkIcon;

    private SpriteRenderer fruitSprite;
    private bool isMerged = false;
    private float delay;

    [Header("VFX Prefabs")]
    [SerializeField] private GameObject landVfxPrefab;
    [SerializeField] private GameObject mergeVfxPrefab;
    private bool hasLanded = false; // Tracks if it has hit ground/other fruit once

    // Property to retrieve the scale defined in the FruitSO ScriptableObject
    public Vector3 TargetScale
    {
        get
        {
            if (fruitData == null) return Vector3.one;

            // Use the manual scale value from the ScriptableObject
            float scaleValue = fruitData.FruitScale * 1.2f;
            return new Vector3(scaleValue, scaleValue, 1f);
        }
    }

    private void Start()
    {
        delay = 0.5f;
        fruitSprite = GetComponent<SpriteRenderer>();
        // Automatically apply the scale from the ScriptableObject
        transform.localScale = TargetScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        ////if (otherFruit != null)
        ////{
        ////    StartCoroutine(Blink());
        ////}
        //if (otherFruit == null) return;

        //if (fruitData != null && otherFruit.Data != null && fruitData.FruitId == otherFruit.Data.FruitId)
        //{
        //    if (isMerged || otherFruit.isMerged) return;

        //    isMerged = true;
        //    otherFruit.isMerged = true;

        //    if (MergeManager.Instance != null)
        //    {
        //        MergeManager.Instance.MergeFruits(this, otherFruit);
        //    }
        //    else
        //    {
        //        Debug.LogError("MergeManager Instance is missing in the scene!");
        //    }
        // --- 1. INSTANTIATE LANDING/COLLISION VFX ---
        if (!hasLanded && !collision.collider.isTrigger)
        {
            hasLanded = true;
            if (landVfxPrefab != null)
            {
                // Spawn at the exact contact point if possible, otherwise at the center
                Vector3 spawnPos = (collision.contactCount > 0) ? collision.GetContact(0).point : transform.position;
                Debug.Log("Tried to load VFX!");
                GameObject vfx = Instantiate(landVfxPrefab, spawnPos, Quaternion.identity);
                Destroy(vfx, 2f); // Automatically clean up after 2 seconds
            }
        }
        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
        if (otherFruit == null) return;
        if (fruitData != null && otherFruit.Data != null && fruitData.FruitId == otherFruit.Data.FruitId)
        {
            if (isMerged || otherFruit.isMerged) return;
            isMerged = true;
            otherFruit.isMerged = true;
            // --- 2. INSTANTIATE MERGE VFX ---
            if (mergeVfxPrefab != null)
            {
                // Spawn at the midpoint between the two merging fruits
                Vector3 mergePos = (transform.position + otherFruit.transform.position) / 2f;
                GameObject vfx = Instantiate(mergeVfxPrefab, mergePos, Quaternion.identity);
                Destroy(vfx, 2f); // Automatically clean up after 2 seconds
            }
            if (MergeManager.Instance != null)
            {
                Vibrations.Haptic(HapticTypes.HeavyImpact);
                Debug.Log("Vibration Worked!");
                MergeManager.Instance.MergeFruits(this, otherFruit);
            }
            else
            {
                Debug.LogError("MergeManager Instance is missing in the scene!");
            }
        }


    }
    }

    

    //public IEnumerator Blink()
    //{
    //    fruitSprite.sprite = blinkIcon;
    //    yield return new WaitForSeconds(delay);
    //    fruitSprite.sprite = defaultIcon;
    //}
