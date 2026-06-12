using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimations : MonoBehaviour
{
    

    // =====================================================
    // INSPECTOR CONFIG
    // =====================================================
    [System.Serializable]
    public class ButtonConfig
    {
        public RectTransform rect;
        public Vector2 targetSize = new Vector2(800f, 300f);
        public float duration = 2f;
    }

    [Header("Buttons")]
    [SerializeField] private ButtonConfig[] buttons;

    // =====================================================
    // RUNTIME CACHE
    // =====================================================
    private class RuntimeButton
    {
        public RectTransform rect;
        public TMP_Text text;
        public Animator animator;
        public Vector2 targetSize;
        public float duration;
    }

    private List<RuntimeButton> runtimeButtons = new List<RuntimeButton>();

    // =====================================================
    // UNITY LIFECYCLE
    // =====================================================
    private void Awake()
    {
        CacheButtons();
    }

    private void Start()
    {
        StartAnimation();
    }

    // =====================================================
    // CACHE SYSTEM
    // =====================================================
    private void CacheButtons()
    {
        runtimeButtons.Clear();

        foreach (var btn in buttons)
        {
            if (btn.rect == null) continue;

            RuntimeButton runtime = new RuntimeButton
            {
                rect = btn.rect,
                text = btn.rect.GetComponentInChildren<TMP_Text>(true),
                animator = btn.rect.GetComponent<Animator>(),
                targetSize = btn.targetSize,
                duration = Mathf.Max(0.05f, btn.duration)
            };

            runtimeButtons.Add(runtime);

            // INITIAL STATE
            runtime.rect.sizeDelta = Vector2.zero;

            if (runtime.text != null)
                runtime.text.gameObject.SetActive(false);

            if (runtime.animator != null)
                runtime.animator.enabled = false;
        }
    }

    // =====================================================
    // START ANIMATION
    // =====================================================
    public void StartAnimation()
    {
        foreach (var btn in runtimeButtons)
        {
            StartCoroutine(AnimateButton(btn));
        }
    }

    // =====================================================
    // CORE ANIMATION
    // =====================================================
    private IEnumerator AnimateButton(RuntimeButton btn)
    {
        Vector2 startSize = Vector2.zero;
        Vector2 endSize = btn.targetSize;

        float time = 0f;

        btn.rect.sizeDelta = startSize;

        while (time < btn.duration)
        {
            time += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(time / btn.duration);

            // smoothstep easing
            t = t * t * (3f - 2f * t);

            btn.rect.sizeDelta = Vector2.LerpUnclamped(startSize, endSize, t);

            yield return null;
        }

        btn.rect.sizeDelta = endSize;

        // ENABLE TEXT AFTER ANIMATION
        if (btn.text != null)
            btn.text.gameObject.SetActive(true);

        // ENABLE ANIMATOR AFTER ANIMATION
        if (btn.animator != null)
            btn.animator.enabled = true;
    }

}
