using System;
using NaughtyAttributes;
using UnityEngine;



public class UnitHp : MonoBehaviour
{
    [SerializeField] private float maxHp;

    [Header("Runtime Debugging")]
    [ReadOnly]
    [SerializeField] private float currentHp;

    public event Action<float, float> OnChanged;
    // public event Action<UnitHpEventArgs> OnChanged1;
    public event Action OnDied;

    private void Awake()
    {
        currentHp = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            ApplyDamage(damageDealer.Damage);
        }
    }

    public float debugDamage;
    [Button()]
    private void ApplyDamageDebug()
    {
        ApplyDamage(debugDamage);
    }
    
    private void ApplyDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(0, currentHp);

        OnChanged?.Invoke(currentHp, maxHp);
        
        // OnChanged1?.Invoke(new UnitHpEventArgs
        // {
        //     CurrentHp = currentHp,
        //     MaxHp = maxHp
        // });

        if (Mathf.Approximately(currentHp, 0))
        {
            OnDied?.Invoke();
        }
    }
}
