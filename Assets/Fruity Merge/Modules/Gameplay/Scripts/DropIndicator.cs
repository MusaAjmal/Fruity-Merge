using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DropIndicator : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fruitRadius = 0.5f;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private LayerMask hitMask;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector2 startPos = spawnPoint.position;

        RaycastHit2D hit = Physics2D.CircleCast(
            startPos,
            fruitRadius,
            Vector2.down,
            maxDistance,
            hitMask
        );

        Vector2 endPos;

        if (hit.collider != null)
        {
            // Center position where fruit would stop
            endPos = hit.centroid;
        }
        else
        {
            endPos = startPos + Vector2.down * maxDistance;
        }

        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    
}
