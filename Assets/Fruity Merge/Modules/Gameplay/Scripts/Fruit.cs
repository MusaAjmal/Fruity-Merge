using Voodoo.Utils;
using System.Collections;
using UnityEngine;

public  enum fruitCollisionSounds
{
    None,
    Background_Music,
    Fruit_Explosion,
    Fruit_Merge


}






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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    // --- 1. INSTANTIATE LANDING/COLLISION VFX ---
    //    if (!hasLanded && !collision.collider.isTrigger)
    //    {

    //        hasLanded = true;
    //        if (landVfxPrefab != null)
    //        {
    //            // Spawn at the exact contact point if possible, otherwise at the center
    //            Vector3 spawnPos = (collision.contactCount > 0) ? collision.GetContact(0).point : transform.position;
    //            Debug.Log("Tried to load VFX!");
    //            SoundManager.instance.PlayOneShotSound(fruitCollisionSounds.Fruit_Explosion.ToString());
    //            GameObject vfx = Instantiate(landVfxPrefab, spawnPos, Quaternion.identity);
    //            Destroy(vfx, 2f); // Automatically clean up after 2 seconds
    //        }
    //    }
    //    Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
    //    if (otherFruit == null) return;

    //    if (fruitData != null && otherFruit.Data != null && fruitData.FruitId == otherFruit.Data.FruitId)
    //    {
    //        if (isMerged || otherFruit.isMerged) return;
    //        isMerged = true;
    //        otherFruit.isMerged = true;
    //        // --- 2. INSTANTIATE MERGE VFX ---
    //        if (mergeVfxPrefab != null)
    //        {
    //            // Spawn at the midpoint between the two merging fruits
    //            Vector3 mergePos = (transform.position + otherFruit.transform.position) / 2f;
    //            GameObject vfx = Instantiate(mergeVfxPrefab, mergePos, Quaternion.identity);
    //            Destroy(vfx, 2f); // Automatically clean up after 2 seconds
    //        }
    //        if (MergeManager.Instance != null)
    //        {
    //            Vibrations.Haptic(HapticTypes.HeavyImpact);
    //            Debug.Log("Vibration Worked!");
    //            MergeManager.Instance.MergeFruits(this, otherFruit);
    //            SoundManager.instance.PlayOneShotSound(fruitCollisionSounds.Fruit_Merge.ToString());
    //        }
    //        else
    //        {
    //            Debug.LogError("MergeManager Instance is missing in the scene!");
    //        }
    //    }


    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        // Check if this collision will result in a merge
        bool isMergeCollision =
            otherFruit != null &&
            fruitData != null &&
            otherFruit.Data != null &&
            fruitData.FruitId == otherFruit.Data.FruitId;

        // --- PLAY LANDING VFX/SFX ONLY FOR NON-MERGE COLLISIONS ---
        if (!hasLanded && !collision.collider.isTrigger && !isMergeCollision)
        {
            hasLanded = true;

            if (landVfxPrefab != null)
            {
                Vector3 spawnPos = (collision.contactCount > 0)
                    ? collision.GetContact(0).point
                    : transform.position;

                Debug.Log("Tried to load VFX!");

                SoundManager.instance.PlayOneShotSound(
                    fruitCollisionSounds.Fruit_Explosion.ToString());

                GameObject vfx = Instantiate(
                    landVfxPrefab,
                    spawnPos,
                    Quaternion.identity);

                Destroy(vfx, 2f);
            }
        }

        // No fruit involved, so we're done
        if (otherFruit == null) return;

        // --- MERGE LOGIC ---
        if (fruitData != null &&
            otherFruit.Data != null &&
            fruitData.FruitId == otherFruit.Data.FruitId)
        {
            if (isMerged || otherFruit.isMerged)
                return;

            isMerged = true;
            otherFruit.isMerged = true;

            // Spawn merge VFX
            if (mergeVfxPrefab != null)
            {
                Vector3 mergePos =
                    (transform.position + otherFruit.transform.position) / 2f;

                GameObject vfx = Instantiate(
                    mergeVfxPrefab,
                    mergePos,
                    Quaternion.identity);
                Debug.Log(vfx.name + "is instantiated at: " + vfx.transform.position);
                Destroy(vfx, 2f);
            }

            if (MergeManager.Instance != null)
            {
                Debug.Log("can Vibrate: " + Vibrations.canVibrate);
                if (Vibrations.canVibrate)
                {
                    Vibrations.Haptic(HapticTypes.Success);
                }
               
                Debug.Log("Vibration Worked!");

                MergeManager.Instance.MergeFruits(this, otherFruit);

                SoundManager.instance.PlayOneShotSound(
                    fruitCollisionSounds.Fruit_Merge.ToString());
            }
            else
            {
                Debug.LogError("MergeManager Instance is missing in the scene!");
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Collision Happened with: " + collision.gameObject.name);
    //}


}

    

    //public IEnumerator Blink()
    //{
    //    fruitSprite.sprite = blinkIcon;
    //    yield return new WaitForSeconds(delay);
    //    fruitSprite.sprite = defaultIcon;
    //}
