using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class Animatable : MonoBehaviour
{
    [Header("Child")]
    [SerializeField] private Animatable[] animatables;

    [Header("Components")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Moving")]
    [SerializeField] private bool needMove;
    [SerializeField] private Vector3 showLocalPosition;
    [SerializeField] private float showPositionDuration = 0.5f;
    [SerializeField] private Ease showPositionEase = Ease.Linear;
    [SerializeField] private Vector3 hideLocalPosition;
    [SerializeField] private float hidePositionDuration = 0.35f;
    [SerializeField] private Ease hidePositionEase = Ease.Linear;

    [Header("Fading")]
    [SerializeField] private bool needFade;
    [SerializeField] private float showAlpha = 1f;
    [SerializeField] private float showFadeDuration = 0.5f;
    [SerializeField] private Ease showFadeEase = Ease.Linear;
    [SerializeField] private float hideAlpha;
    [SerializeField] private float hideFadeDuration = 0.35f;
    [SerializeField] private Ease hideFadeEase = Ease.Linear;

    [Header("Rotation")]
    [SerializeField] private bool needRotation;
    [SerializeField] private Vector3 showRotation;
    [SerializeField] private float showRotationDuration = 0.5f;
    [SerializeField] private Ease showRotationEase = Ease.Linear;
    [SerializeField] private Vector3 hideRotation;
    [SerializeField] private float hideRotationDuration = 0.35f;
    [SerializeField] private Ease hideRotationEase = Ease.Linear;

    private Sequence sequence;

    [Button()]
    public void Show()
    {
        Animate(true);

        CallInAnimatables(animatable => animatable.Show());
    }

    [Button()]
    public void Hide()
    {
        Animate(false);

        CallInAnimatables(animatable => animatable.Hide());
    }

    public void SetHidePosition(Vector3 hidePosition)
    {
        hideLocalPosition = hidePosition;
    }

    private void Animate(bool isShow)
    {
        if (!needMove && !needFade && !needRotation)
        {
            return;
        }

        sequence?.Kill();

        sequence = DOTween.Sequence();

        if (needMove)
        {
            var pos = isShow ? showLocalPosition : hideLocalPosition;
            var time = isShow ? showPositionDuration : hidePositionDuration;
            var ease = isShow ? showPositionEase : hidePositionEase;
            sequence.Join(rectTransform.DOAnchorPos(pos, time).SetEase(ease));
        }

        if (needFade)
        {
            var alpha = isShow ? showAlpha : hideAlpha;
            var time = isShow ? showFadeDuration : hideFadeDuration;
            var ease = isShow ? showFadeEase : hideFadeEase;
            sequence.Join(canvasGroup.DOFade(alpha, time).SetEase(ease));
        }

        if (needRotation)
        {
            var rotation = isShow ? showRotation : hideRotation;
            var time = isShow ? showRotationDuration : hideRotationDuration;
            var ease = isShow ? showRotationEase : hideRotationEase;
            sequence.Join(rectTransform.DOLocalRotate(rotation, time).SetEase(ease));
        }
    }

    private void CallInAnimatables(Action<Animatable> action)
    {
        if (animatables == null) return;

        foreach (var animatable in animatables)
        {
            action?.Invoke(animatable);
        }
    }
}
