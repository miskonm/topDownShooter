using System;
using Lean.Pool;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float shootDelay;
    [SerializeField] private Animator animator;
    [SerializeField] private string shootTriggerName;
    [SerializeField] private int hp;

    [SerializeField] private int currentHp;
    

    private float currentShootDelay;

    private void Awake()
    {
        currentHp = hp;
    }

    private void Update()
    {
        Shoot();
    }

    public void ChangeHealth(int amount)
    {
        currentHp += amount;

        if (currentHp < 0)
        {
            Debug.Log($"DIED");
        }
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire1") && currentShootDelay <= 0)
        {
            currentShootDelay = shootDelay;
            CreateBullet();
            PlayShootAnimation();
        }

        currentShootDelay -= Time.deltaTime;
    }

    private void PlayShootAnimation()
    {
        animator.SetTrigger(shootTriggerName);
    }

    private void CreateBullet()
    {
        LeanPool.Spawn(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
    }
}
