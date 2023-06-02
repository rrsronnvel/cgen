using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float nextFire = 0f;

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            FireBullet();
        }
    }

    void FireBullet()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
