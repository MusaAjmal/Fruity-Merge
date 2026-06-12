using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float punchScale = 1.12f;
    [SerializeField] private float punchDuration = 0.1f;
    [SerializeField] private float resetDuration = 0.15f;

    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * punchScale, punchDuration)
                 .setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, resetDuration)
                 .setEase(LeanTweenType.easeOutBack);
    }
}
