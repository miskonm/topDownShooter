using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float shootDelay;
    [SerializeField] private Animator animator;
    [SerializeField] private string shootTriggerName;

    private float currentShootDelay;

    private void Update()
    {
        Shoot();
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
        Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
    }
}
