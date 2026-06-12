using UnityEngine;

public class TextHovering : MonoBehaviour
{

    [SerializeField] private float moveDistance = 20f;
    [SerializeField] private float duration = 0.2f;

[SerializeField]   private RectTransform rect;

    private Vector3 startPos;

    //uses lean tween library to add text hovering
    private void Start()
    {
        //rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;

        LeanTween.moveX(rect, startPos.x + moveDistance, duration)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong();
    }

}
