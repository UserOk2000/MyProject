using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    [SerializeField] private int _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Wall _))
        {
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }
}
