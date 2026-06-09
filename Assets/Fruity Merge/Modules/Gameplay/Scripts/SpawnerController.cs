//using System.Collections.Generic;
//using UnityEngine;
//using System.Collections;

//public class SpawnerController : MonoBehaviour
//{
//    [SerializeField] private List<Fruit> fruits;
//    [SerializeField] private SpriteRenderer previewRenderer;
//    [SerializeField] private Transform spawnPosition;
//    [SerializeField] private float edgeMargin = 0.6f;

//    private Camera cam;
//    private int nextIndex;
//    private bool isDragging;
//    private bool canSpawn = true;

//    private Vector3 nextFruitScale = Vector3.one;

//    private void Start()
//    {
//        cam = Camera.main;
//        previewRenderer = GetComponent<SpriteRenderer>();
//        PickNextFruit();
//    }

//    private void Update()
//    {
//        HandleInput();
//    }

//    private void HandleInput()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            isDragging = true;
//        }

//        if (isDragging && Input.GetMouseButton(0))
//        {
//            MoveSpawner();
//        }

//        if (Input.GetMouseButtonUp(0))
//        {
//            isDragging = false;

//            if (canSpawn)
//            {
//                StartCoroutine(SpawnAfterDelay());
//            }
//        }
//    }

//    private void MoveSpawner()
//    {
//        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);

//        float halfWidth = cam.orthographicSize * cam.aspect;

//        float minX = cam.transform.position.x - halfWidth + edgeMargin;
//        float maxX = cam.transform.position.x + halfWidth - edgeMargin;

//        float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);

//        transform.position = new Vector3(
//            clampedX,
//            transform.position.y,
//            transform.position.z
//        );
//    }

//    private IEnumerator SpawnAfterDelay()
//    {
//        canSpawn = false;

//        yield return new WaitForSeconds(0.2f);

//        Instantiate(fruits[nextIndex], spawnPosition.position, Quaternion.identity);

//        // clear preview immediately after spawn
//        previewRenderer.sprite = null;

//        // optional: reset scale so it doesn't stay stretched
//        previewRenderer.transform.localScale = Vector3.one;

//        PickNextFruit();

//        canSpawn = true;
//    }

//    private void PickNextFruit()
//    {
//        if (fruits == null || fruits.Count == 0)
//        {
//            Debug.LogError("Fruits list is empty!");
//            return;
//        }

//        nextIndex = Random.Range(0, fruits.Count);

//        nextFruitScale = fruits[nextIndex].transform.localScale;

//        StartCoroutine(UpdatePreviewAfterDelay());
//    }

//    private IEnumerator UpdatePreviewAfterDelay()
//    {
//        yield return new WaitForSeconds(0.8f);

//        if (previewRenderer != null)
//        {
//            previewRenderer.sprite =
//                fruits[nextIndex].GetComponent<SpriteRenderer>().sprite;

//            // match visual size of spawned fruit
//            previewRenderer.transform.localScale = nextFruitScale;
//        }
//    }
//}

//using System.Collections.Generic;
//using UnityEngine;
//using System.Collections;

//public class SpawnerController : MonoBehaviour
//{
//    [SerializeField] private List<Fruit> fruits;
//    [SerializeField] private SpriteRenderer previewRenderer;
//    [SerializeField] private Transform spawnPosition;
//    [SerializeField] private float edgeMargin = 0.6f;

//    private Camera cam;
//    private int nextIndex;
//    private bool isDragging;
//    private bool canSpawn = true;

//    private Vector3 nextFruitScale = Vector3.one;

//    private void Start()
//    {
//        cam = Camera.main;
//        previewRenderer = GetComponent<SpriteRenderer>();
//        PickNextFruit();
//    }

//    private void Update()
//    {
//        HandleInput();
//    }

//    private void HandleInput()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            isDragging = true;
//        }

//        if (isDragging && Input.GetMouseButton(0))
//        {
//            MoveSpawner();
//        }

//        if (Input.GetMouseButtonUp(0))
//        {
//            isDragging = false;

//            if (canSpawn)
//            {
//                StartCoroutine(SpawnAfterDelay());
//            }
//        }
//    }

//    private void MoveSpawner()
//    {
//        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);

//        float halfWidth = cam.orthographicSize * cam.aspect;

//        float minX = cam.transform.position.x - halfWidth + edgeMargin;
//        float maxX = cam.transform.position.x + halfWidth - edgeMargin;

//        float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);

//        transform.position = new Vector3(
//            clampedX,
//            transform.position.y,
//            transform.position.z
//        );
//    }

//    private IEnumerator SpawnAfterDelay()
//    {
//        canSpawn = false;

//        yield return new WaitForSeconds(0.2f);

//        GameObject spawnedFruit = Instantiate(fruits[nextIndex].gameObject, spawnPosition.position, Quaternion.identity);

//        // Apply scale immediately to the spawned instance
//        Fruit fruitComponent = spawnedFruit.GetComponent<Fruit>();
//        if (fruitComponent != null)
//        {
//            spawnedFruit.transform.localScale = fruitComponent.TargetScale;
//        }

//        // clear preview immediately after spawn
//        previewRenderer.sprite = null;

//        // reset preview scale so it doesn't stay stretched
//        previewRenderer.transform.localScale = Vector3.one;

//        PickNextFruit();

//        canSpawn = true;
//    }

//    private void PickNextFruit()
//    {
//        if (fruits == null || fruits.Count == 0)
//        {
//            Debug.LogError("Fruits list is empty!");
//            return;
//        }

//        nextIndex = Random.Range(0, fruits.Count);

//        // FETCH DYNAMIC SCALE: Uses the dynamic TargetScale property from the Fruit component
//        nextFruitScale = fruits[nextIndex].TargetScale;

//        StartCoroutine(UpdatePreviewAfterDelay());
//    }

//    private IEnumerator UpdatePreviewAfterDelay()
//    {
//        yield return new WaitForSeconds(0.8f);

//        if (previewRenderer != null)
//        {
//            previewRenderer.sprite =
//                fruits[nextIndex].GetComponent<SpriteRenderer>().sprite;

//            // match visual size of spawned fruit
//            previewRenderer.transform.localScale = nextFruitScale;
//        }
//    }
//}
//using System.Collections.Generic;
//using UnityEngine;
//using System.Collections;

//public class SpawnerController : MonoBehaviour
//{
//    [Header("References")]
//    [SerializeField] private FruitDatabase fruitDatabase;
//    [SerializeField] private SpriteRenderer previewRenderer;
//    [SerializeField] private Transform spawnPosition;

//    [Header("Spawn Settings")]
//    [SerializeField] private float edgeMargin = 0.6f;
//    [SerializeField] private int minSpawnId = 1;
//    [SerializeField] private int maxSpawnId = 8;

//    private Camera cam;
//    private FruitSO nextFruitData;
//    private bool isDragging;
//    private bool canSpawn = true;

//    private Vector3 nextFruitScale = Vector3.one;
//    private List<FruitSO> spawnableFruits = new List<FruitSO>();

//    private void Start()
//    {
//        cam = Camera.main;
//        previewRenderer = GetComponent<SpriteRenderer>();

//        FilterSpawnableFruits();
//        PickNextFruit();
//    }

//    private void Update()
//    {
//        HandleInput();
//    }

//    /// <summary>
//    /// Filters fruits from the database that match the allowed ID range (1 to 8).
//    /// </summary>
//    private void FilterSpawnableFruits()
//    {
//        if (fruitDatabase == null)
//        {
//            Debug.LogError("Fruit Database is not assigned to SpawnerController!");
//            return;
//        }

//        spawnableFruits.Clear();
//        foreach (FruitSO fruit in fruitDatabase.Fruits)
//        {
//            if (fruit != null && fruit.FruitId >= minSpawnId && fruit.FruitId <= maxSpawnId)
//            {
//                spawnableFruits.Add(fruit);
//            }
//        }

//        if (spawnableFruits.Count == 0)
//        {
//            Debug.LogError($"No fruits found in Database with ID between {minSpawnId} and {maxSpawnId}!");
//        }
//    }

//    private void HandleInput()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            isDragging = true;
//        }

//        if (isDragging && Input.GetMouseButton(0))
//        {
//            MoveSpawner();
//        }

//        if (Input.GetMouseButtonUp(0))
//        {
//            isDragging = false;

//            if (canSpawn && nextFruitData != null)
//            {
//                StartCoroutine(SpawnAfterDelay());
//            }
//        }
//    }

//    private void MoveSpawner()
//    {
//        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);

//        float halfWidth = cam.orthographicSize * cam.aspect;

//        float minX = cam.transform.position.x - halfWidth + edgeMargin;
//        float maxX = cam.transform.position.x + halfWidth - edgeMargin;

//        float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);

//        transform.position = new Vector3(
//            clampedX,
//            transform.position.y,
//            transform.position.z
//        );
//    }

//    private IEnumerator SpawnAfterDelay()
//    {
//        canSpawn = false;

//        yield return new WaitForSeconds(0.2f);

//        if (nextFruitData != null && nextFruitData.Prefab != null)
//        {
//            GameObject spawnedFruit = Instantiate(nextFruitData.Prefab, spawnPosition.position, Quaternion.identity);

//            // Apply calculated scale immediately to the spawned instance
//            Fruit fruitComponent = spawnedFruit.GetComponent<Fruit>();
//            if (fruitComponent != null)
//            {
//                spawnedFruit.transform.localScale = fruitComponent.TargetScale;
//            }
//        }

//        // clear preview immediately after spawn
//        previewRenderer.sprite = null;

//        // reset scale so it doesn't stay stretched
//        previewRenderer.transform.localScale = Vector3.one;

//        PickNextFruit();

//        canSpawn = true;
//    }

//    private void PickNextFruit()
//    {
//        if (spawnableFruits.Count == 0)
//        {
//            Debug.LogError("No spawnable fruits available!");
//            return;
//        }

//        int nextIndex = Random.Range(0, spawnableFruits.Count);
//        nextFruitData = spawnableFruits[nextIndex];

//        // Fetch dynamic TargetScale from the prefab's Fruit component
//        if (nextFruitData.Prefab != null)
//        {
//            Fruit prefabFruit = nextFruitData.Prefab.GetComponent<Fruit>();
//            if (prefabFruit != null)
//            {
//                nextFruitScale = prefabFruit.TargetScale;
//            }
//        }

//        StartCoroutine(UpdatePreviewAfterDelay());
//    }

//    private IEnumerator UpdatePreviewAfterDelay()
//    {
//        yield return new WaitForSeconds(0.8f);

//        if (previewRenderer != null && nextFruitData != null && nextFruitData.Prefab != null)
//        {
//            SpriteRenderer prefabRenderer = nextFruitData.Prefab.GetComponent<SpriteRenderer>();
//            if (prefabRenderer != null)
//            {
//                previewRenderer.sprite = prefabRenderer.sprite;
//            }

//            // match visual size of spawned fruit
//            previewRenderer.transform.localScale = nextFruitScale;
//        }
//    }
//}


//using System.Collections.Generic;
//using UnityEngine;
//using System.Collections;

//public class SpawnerController : MonoBehaviour
//{
//    [Header("References")]
//    [SerializeField] private FruitDatabase fruitDatabase;
//    [SerializeField] private SpriteRenderer previewRenderer;
//    [SerializeField] private Transform spawnPosition;

//    [Header("Spawn Settings")]
//    [SerializeField] private float edgeMargin = 0.6f;
//    [SerializeField] private int minSpawnId = 1;
//    [SerializeField] private int maxSpawnId = 8;
//    [SerializeField] private float spawnCooldown = 0.8f;

//    private Camera cam;
//    private FruitSO nextFruitData;
//    private bool isDragging;
//    private bool canSpawn = true;

//    private Vector3 nextFruitScale = Vector3.one;
//    private List<FruitSO> spawnableFruits = new List<FruitSO>();

//    private void Start()
//    {
//        cam = Camera.main;
//        previewRenderer = GetComponent<SpriteRenderer>();

//        FilterSpawnableFruits();
//        PickNextFruit();
//    }

//    private void Update()
//    {
//        HandleInput();
//    }

//    private void FilterSpawnableFruits()
//    {
//        if (fruitDatabase == null)
//        {
//            Debug.LogError("Fruit Database is not assigned!");
//            return;
//        }

//        spawnableFruits.Clear();

//        foreach (FruitSO fruit in fruitDatabase.Fruits)
//        {
//            if (fruit != null &&
//                fruit.FruitId >= minSpawnId &&
//                fruit.FruitId <= maxSpawnId)
//            {
//                spawnableFruits.Add(fruit);
//            }
//        }

//        if (spawnableFruits.Count == 0)
//        {
//            Debug.LogError("No spawnable fruits found in range!");
//        }
//    }

//    private void HandleInput()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            isDragging = true;
//        }

//        if (isDragging && Input.GetMouseButton(0))
//        {
//            MoveSpawner();
//        }

//        if (Input.GetMouseButtonUp(0))
//        {
//            isDragging = false;

//            if (canSpawn && nextFruitData != null)
//            {
//                StartCoroutine(SpawnWithCooldown());
//            }
//        }
//    }

//    private void MoveSpawner()
//    {
//        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);

//        float halfWidth = cam.orthographicSize * cam.aspect;

//        float minX = cam.transform.position.x - halfWidth + edgeMargin;
//        float maxX = cam.transform.position.x + halfWidth - edgeMargin;

//        float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);

//        transform.position = new Vector3(
//            clampedX,
//            transform.position.y,
//            transform.position.z
//        );
//    }

//    private IEnumerator SpawnWithCooldown()
//    {
//        canSpawn = false;

//        // small spawn feel delay
//        yield return new WaitForSeconds(0.2f);

//        if (nextFruitData != null && nextFruitData.Prefab != null)
//        {
//            GameObject spawnedFruit = Instantiate(
//                nextFruitData.Prefab,
//                spawnPosition.position,
//                Quaternion.identity
//            );

//            Fruit fruitComponent = spawnedFruit.GetComponent<Fruit>();
//            if (fruitComponent != null)
//            {
//                spawnedFruit.transform.localScale = fruitComponent.TargetScale;
//            }
//        }

//        // clear preview
//        previewRenderer.sprite = null;
//        previewRenderer.transform.localScale = Vector3.one;

//        PickNextFruit();

//        // FULL COOLDOWN (0.8s total rule)
//        yield return new WaitForSeconds(spawnCooldown);

//        canSpawn = true;
//    }

//    private void PickNextFruit()
//    {
//        if (spawnableFruits.Count == 0)
//        {
//            Debug.LogError("No spawnable fruits available!");
//            return;
//        }

//        int nextIndex = Random.Range(0, spawnableFruits.Count);
//        nextFruitData = spawnableFruits[nextIndex];

//        if (nextFruitData.Prefab != null)
//        {
//            Fruit prefabFruit = nextFruitData.Prefab.GetComponent<Fruit>();

//            if (prefabFruit != null)
//            {
//                nextFruitScale = prefabFruit.TargetScale;
//            }
//        }

//        StartCoroutine(UpdatePreviewAfterDelay());
//    }

//    private IEnumerator UpdatePreviewAfterDelay()
//    {
//        yield return new WaitForSeconds(0.8f);

//        if (previewRenderer != null &&
//            nextFruitData != null &&
//            nextFruitData.Prefab != null)
//        {
//            SpriteRenderer prefabRenderer =
//                nextFruitData.Prefab.GetComponent<SpriteRenderer>();

//            if (prefabRenderer != null)
//            {
//                previewRenderer.sprite = prefabRenderer.sprite;
//            }

//            previewRenderer.transform.localScale = nextFruitScale;
//        }
//    }
//}



using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; // Required for checking UI collisions

public class SpawnerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FruitDatabase fruitDatabase;
    [SerializeField] private SpriteRenderer previewRenderer;
    [SerializeField] private Transform spawnPosition;

    // Assign the collider from your lineOfSpawn GameObject here (Ensure 'Is Trigger' is ticked on it)
    //[SerializeField] private Collider2D lineOfSpawnCollider;

    [Header("Spawn Settings")]
    [SerializeField] private float edgeMargin = 0.6f;
    [SerializeField] private int minSpawnId = 1;
    [SerializeField] private int maxSpawnId = 8;
    [SerializeField] private float spawnCooldown = 0.8f;

    private Camera cam;
    private FruitSO nextFruitData;
    private bool isDragging;
    private bool canSpawn = true;

    private Vector3 nextFruitScale = Vector3.one;
    private List<FruitSO> spawnableFruits = new List<FruitSO>();

    private void Start()
    {
        cam = Camera.main;
        previewRenderer = GetComponent<SpriteRenderer>();

        FilterSpawnableFruits();
        PickNextFruit();

        // Automatically configure the line collider as a trigger
       
    }

    private void Update()
    {
        HandleInput();
    }

    private void FilterSpawnableFruits()
    {
        if (fruitDatabase == null)
        {
            Debug.LogError("Fruit Database is not assigned!");
            return;
        }

        spawnableFruits.Clear();

        foreach (FruitSO fruit in fruitDatabase.Fruits)
        {
            if (fruit != null &&
                fruit.FruitId >= minSpawnId &&
                fruit.FruitId <= maxSpawnId)
            {
                spawnableFruits.Add(fruit);
            }
        }

        if (spawnableFruits.Count == 0)
        {
            Debug.LogError("No spawnable fruits found in range!");
        }
    }

    private void HandleInput()
    {
        // 1. When clicking down, check if the click started on top of a UI element.
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI()) return; // Ignore input completely if clicking UI
            isDragging = true;
        }

        // 2. Only move the spawner if we started dragging in bounds (not on UI)
        if (isDragging && Input.GetMouseButton(0))
        {
            MoveSpawner();
        }

        // 3. Only spawn when releasing the mouse if we were actively dragging
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;

                if (canSpawn && nextFruitData != null)
                {
                    StartCoroutine(SpawnWithCooldown());
                }
            }
        }
    }

    private void MoveSpawner()
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        float halfWidth = cam.orthographicSize * cam.aspect;

        float minX = cam.transform.position.x - halfWidth + edgeMargin;
        float maxX = cam.transform.position.x + halfWidth - edgeMargin;

        float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);

        transform.position = new Vector3(
            clampedX,
            transform.position.y,
            transform.position.z
        );
    }

    private IEnumerator SpawnWithCooldown()
    {
        canSpawn = false;

        // Small spawn feel delay
        yield return new WaitForSeconds(0.2f);

        if (nextFruitData != null && nextFruitData.Prefab != null)
        {
            GameObject spawnedFruit = Instantiate(
                nextFruitData.Prefab,
                spawnPosition.position,
                Quaternion.identity
            );

            // Add the FruitTracker dynamically to track when it lands/collisions
            FruitTracker tracker = spawnedFruit.AddComponent<FruitTracker>();

            Fruit fruitComponent = spawnedFruit.GetComponent<Fruit>();

            if (fruitComponent != null)
            {
                spawnedFruit.transform.localScale = fruitComponent.TargetScale;
            }
        }

        // Clear preview
        previewRenderer.sprite = null;
        previewRenderer.transform.localScale = Vector3.one;

        PickNextFruit();

        // Spawn cooldown
        yield return new WaitForSeconds(spawnCooldown);

        canSpawn = true;
    }

    private void PickNextFruit()
    {
        if (spawnableFruits.Count == 0)
        {
            Debug.LogError("No spawnable fruits available!");
            return;
        }

        int nextIndex = Random.Range(0, spawnableFruits.Count);
        nextFruitData = spawnableFruits[nextIndex];

        if (nextFruitData.Prefab != null)
        {
            Fruit prefabFruit = nextFruitData.Prefab.GetComponent<Fruit>();

            if (prefabFruit != null)
            {
                nextFruitScale = prefabFruit.TargetScale;
            }
        }

        StartCoroutine(UpdatePreviewAfterDelay());
    }

    private IEnumerator UpdatePreviewAfterDelay()
    {
        yield return new WaitForSeconds(0.8f);

        if (previewRenderer != null &&
            nextFruitData != null &&
            nextFruitData.Prefab != null)
        {
            SpriteRenderer prefabRenderer =
                nextFruitData.Prefab.GetComponent<SpriteRenderer>();

            if (prefabRenderer != null)
            {
                previewRenderer.sprite = prefabRenderer.sprite;
            }

            previewRenderer.transform.localScale = nextFruitScale;
        }
    }

    /// <summary>
    /// Helper method to check if the mouse pointer (or touch) is currently over a UI element.
    /// </summary>
    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
        {
            return false;
        }

        // Check for mouse clicks (PC)
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }

        // Check for screen touches (Mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return true;
            }
        }

        return false;
    }
}