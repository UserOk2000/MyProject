using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvashion : MonoBehaviour
{
    [Header("Настройки уклонения")]
    [Range(0f, 100f)]
    [SerializeField] private float dodgeChance = 60f;   // Шанс уклонения в % (например, 60% — увернется, 40% — получит урон)
    [SerializeField] private float dodgeSpeed = 10f;    // Скорость рывка при уклонении
    [SerializeField] private float dodgeDuration = 0.2f; // Длительность рывка в секундах
    [SerializeField] private float dodgeCooldown = 2.0f; // Перезарядка уклонения (время уязвимости врага)

    [Header("Обнаружение угроз")]
    [SerializeField] private float detectionRadius = 4f; // Дистанция, на которой враг замечает пулю
    [SerializeField] private LayerMask bulletLayer;      // Слой, на котором находятся пули игрока

    private Rigidbody2D rb;
    private bool isDodging = false;
    private bool canDodge = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Если враг уже уклоняется или кулдаун не прошел, пропускаем проверку
        if (isDodging || !canDodge) return;

        DetectIncomingBullets();
    }

    void DetectIncomingBullets()
    {
        // Ищем все коллайдеры пуль в радиусе обнаружения вокруг противника
        Collider2D[] hitBullets = Physics2D.OverlapCircleAll(transform.position, detectionRadius, bulletLayer);

        foreach (Collider2D bulletCollider in hitBullets)
        {
            Rigidbody2D bulletRb = bulletCollider.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                // Проверяем, летит ли пуля в сторону противника, а не от него
                Vector2 vectorToEnemy = (transform.position - bulletCollider.transform.position).normalized;
                float approachFactor = Vector2.Dot(bulletRb.velocity.normalized, vectorToEnemy);

                // Если пуля движется в сторону врага (угловое соответствие > 0.7)
                if (approachFactor > 0.7f)
                {
                    TryDodge(bulletRb.velocity);
                    break; // Реагируем на первую же опасную пулю
                }
            }
        }
    }

    void TryDodge(Vector2 bulletVelocity)
    {
        // Проверка на случайный шанс. Если рандом больше шанса уклонения — враг "тупит" и подставляется
        if (Random.Range(0f, 100f) > dodgeChance)
        {
            // Враг не уклоняется, запускаем небольшой кулдаун, чтобы он не проверял эту же пулю каждый кадр
            StartCoroutine(DodgeFailCooldown());
            return;
        }

        // Если шанс успешен, вычисляем направление уклонения (перпендикулярно пуле)
        // В 2D перпендикуляр к вектору (x, y) — это (-y, x) или (y, -x)
        Vector2 dodgeDirection = new Vector2(-bulletVelocity.y, bulletVelocity.x).normalized;

        // Случайно выбираем, уклоняться влево или вправо относительно траектории пули
        if (Random.value > 0.5f)
        {
            dodgeDirection = -dodgeDirection;
        }

        // Запускаем процесс рывка
        StartCoroutine(DodgeRoutine(dodgeDirection));
    }

    IEnumerator DodgeRoutine(Vector2 direction)
    {
        isDodging = true;
        canDodge = false;

        // Применяем скорость рывка
        rb.velocity = direction * dodgeSpeed;

        // Ждем пока длится рывок
        yield return new WaitForSeconds(dodgeDuration);

        // Останавливаем противника после уклонения
        rb.velocity = Vector2.zero;
        isDodging = false;

        // Ждем перезарядку способности, прежде чем враг сможет уклониться снова
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }

    IEnumerator DodgeFailCooldown()
    {
        canDodge = false;
        // Короткая пауза (например, 0.5 сек), во время которой враг гарантированно получит пулю в лицо
        yield return new WaitForSeconds(0.5f);
        canDodge = true;
    }

    // Визуализация радиуса обнаружения в редакторе Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
