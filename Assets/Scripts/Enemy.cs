using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            ApplyDamage(damageDealer.Damage);
        }
    }

    private void ApplyDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
