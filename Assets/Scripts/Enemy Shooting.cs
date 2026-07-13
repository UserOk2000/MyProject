using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Настройки стрельбы")]
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private Transform firePoint;     
    [SerializeField] private float fireRate = 0.8f;    
    [SerializeField] private float scatterAngle = 5f;  

    [Header("Настройки обнаружения")]
    [SerializeField] private float attackRange = 10f;  

    private Transform player;
    private float nextFireTime = 0f;

    void Start()
    {
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Враг поворачивается и стреляет ТОЛЬКО если игрок в зоне атаки/видимости
        if (distanceToPlayer <= attackRange)
        {
            RotateTowardsPlayer();

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void RotateTowardsPlayer()
    {
        
        Vector2 direction = player.position - transform.position;

       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        
        float randomScatter = Random.Range(-scatterAngle, scatterAngle);
        Quaternion bulletRotation = firePoint.rotation * Quaternion.Euler(0, 0, randomScatter);

        
        Instantiate(bulletPrefab, firePoint.position, bulletRotation);
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
