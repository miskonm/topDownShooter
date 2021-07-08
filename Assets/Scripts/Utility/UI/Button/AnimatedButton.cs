using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedButton : Button
{
    private const float ColorRgbValue = 0.7843137f;

    [Header("Animation")]
    [SerializeField] private bool needScaleAnimation;
    [SerializeField] private float touchDownScaleAnimationTime = 0.07f;
    [SerializeField] private float touchDownScaleAnimationScale = 0.95f;
    [SerializeField] private Ease touchDownScaleEase = Ease.Linear;
    [SerializeField] private float touchUpScaleAnimationTime = 0.07f;
    [SerializeField] private float touchUpScaleAnimationScale = 1f;
    [SerializeField] private Ease touchUpScaleEase = Ease.Linear;

    [Header("Color")]
    [SerializeField] private bool needColorChange;
    [SerializeField] private Graphic[] graphicsToChange;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightedColor = Color.white;
    [SerializeField] private Color pressedColor = new Color(ColorRgbValue, ColorRgbValue, ColorRgbValue, 1f);
    [SerializeField] private Color selectedColor = Color.white;
    [SerializeField] private Color disabledColor = new Color(ColorRgbValue, ColorRgbValue, ColorRgbValue, 0.5f);
    [SerializeField] private float colorAnimationTime = 0.1f;

    private Tweener animationTweener;
    private SelectionState previousState;

    private RectTransform scaleAnimationRectTransform;

    protected override void Awake()
    {
        base.Awake();

        scaleAnimationRectTransform = GetComponent<RectTransform>();
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        if (previousState == state)
        {
            return;
        }

        ChangeScaleState(state, instant);
        ChangeColorState(state, instant);

        previousState = state;
    }

    private void ChangeScaleState(SelectionState state, bool instant)
    {
        if (!needScaleAnimation || scaleAnimationRectTransform == null)
        {
            return;
        }

        switch (state)
        {
            case SelectionState.Normal:
            {
                if (previousState == SelectionState.Selected)
                {
                    break;
                }

                ScaleRectTransform(touchUpScaleAnimationScale, touchUpScaleAnimationTime, touchUpScaleEase,
                    SelectionState.Normal);

                break;
            }
            case SelectionState.Selected:
            {
                if (previousState == SelectionState.Normal)
                {
                    break;
                }

                ScaleRectTransform(touchUpScaleAnimationScale, touchUpScaleAnimationTime, touchUpScaleEase,
                    SelectionState.Selected);

                break;
            }
            case SelectionState.Pressed:
            {
                ScaleRectTransform(touchDownScaleAnimationScale, touchDownScaleAnimationTime, touchDownScaleEase,
                    SelectionState.Pressed);

                break;
            }
        }
    }

    private void ScaleRectTransform(float targetScale, float duration, Ease ease, SelectionState selectionState)
    {
        animationTweener.Kill();

        var currentScale = scaleAnimationRectTransform.localScale;

        if (currentScale.Equals(new Vector3(targetScale, targetScale, targetScale)))
        {
            return;
        }

        animationTweener = scaleAnimationRectTransform.DOScale(targetScale, duration).SetEase(ease);
    }

    private void ChangeColorState(SelectionState state, bool isInstant)
    {
        if (!needColorChange) return;

        var color = state switch
        {
            SelectionState.Disabled => disabledColor,
            SelectionState.Highlighted => highlightedColor,
            SelectionState.Normal => normalColor,
            SelectionState.Pressed => pressedColor,
            SelectionState.Selected => selectedColor,
            _ => Color.white
        };

        StartColorAnimation(color, isInstant);
    }

    private void StartColorAnimation(Color endColor, bool isInstant)
    {
        if (graphicsToChange == null) return;

        for (int i = 0, n = graphicsToChange.Length; i < n; i++)
        {
            ChangeGraphicColor(graphicsToChange[i], endColor, isInstant);
        }
    }

    private void ChangeGraphicColor(Graphic graphic, Color endColor, bool isInstant)
    {
        if (graphic == null) return;

        graphic.CrossFadeColor(endColor, isInstant ? 0f : colorAnimationTime, true, true);
    }
}
