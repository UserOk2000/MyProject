using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Настройки пули")]
    [SerializeField] private float speed = 12f;       // Скорость полета пули
    [SerializeField] private float lifeTime = 3f;    // Время жизни пули в секундах (чтобы не летела вечно)
    [SerializeField] private int damage = 1;         // Урон, который наносит пуля

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Задаем пуле скорость в направлении, куда она повернута при спавне.
        // В 2D Top-Down "вперед" для повернутого объекта — это transform.right (ось X).
        rb.velocity = transform.right * speed;

        // Уничтожаем пулю автоматически через lifeTime секунд, если она ни обо что не ударилась
        Destroy(gameObject, lifeTime);
    }

    // Этот метод срабатывает, когда пуля сталкивается с другим коллайдером
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, столкнулась ли пуля с игроком
        if (collision.TryGetComponent(out PlayerHealth playerHealth))
        {
            // Здесь должна быть логика нанесения урона игроку. К примеру:
            // PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            // if (playerHealth != null) { playerHealth.TakeDamage(damage); }

            Debug.Log("Пуля попала в игрока!");

            playerHealth.TakeDamage(damage);
            Destroy(gameObject); // Уничтожаем пулю после попадания
        }
        // Проверяем столкновение со стенами или препятствиями
        else if (collision.CompareTag("Obstacle") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Названия тегов или слоев укажите под свой проект (например, "Walls", "Obstacle" и т.д.)
            Destroy(gameObject);
        }
    }
}

