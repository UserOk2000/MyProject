using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Настройки стрельбы")]
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private Transform firePoint;     
    [SerializeField] private float bulletSpeed = 20f;  

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            
            rb.velocity = firePoint.right * bulletSpeed;
        }
    }
}
