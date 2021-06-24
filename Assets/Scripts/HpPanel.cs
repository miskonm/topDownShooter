using System;
using UnityEngine;
using UnityEngine.UI;

public class HpPanel : MonoBehaviour
{
    [SerializeField] private UnitHp unitHp;
    [SerializeField] private Image image;

    private void OnEnable()
    {
        unitHp.OnChanged += UnitHp_OnChanged;
    }

    private void OnDisable()
    {
        unitHp.OnChanged -= UnitHp_OnChanged;
    }

    private void LateUpdate()
    {
        transform.up = Vector2.up; 
    }

    private void UpdateUi(float currentHp, float maxHp)
    {
        var fillAmount = currentHp / maxHp;
        image.fillAmount = fillAmount;
    }
    
    private void UnitHp_OnChanged(float currentHp, float maxHp)
    {
        UpdateUi(currentHp, maxHp);
    }
}
