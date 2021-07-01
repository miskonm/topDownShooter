using Lean.Pool;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float speed;

    private void OnEnable()
    {
        
    }

    private void OnBecameInvisible()
    {
        KillSelf();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        KillSelf();
    }

    private void KillSelf()
    {
        LeanPool.Despawn(gameObject);
    }

    public void OnSpawn()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up * speed;
    }

    public void OnDespawn()
    {
        
    }
}
