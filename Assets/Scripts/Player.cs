using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float shootDelay;

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
        }

        currentShootDelay -= Time.deltaTime;
    }

    private void CreateBullet()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
    }
}
