using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Зона обнаружения")]
    [SerializeField] private float visionRadius = 8f;   // Радиус, в котором враг замечает игрока
    [SerializeField] private LayerMask playerLayer;     // Слой игрока (должен быть Player)

    [Header("Ссылки на компоненты")]
    [SerializeField] private EnemyPatrol patrolScript;
    [SerializeField] private EnemyEvashion evasionScript;
    [SerializeField] private EnemyShooting shootingScript;
    [SerializeField] private EnemyHealth enemyHealthScript;

    private bool playerSpotted = false;

    void Start()
    {
        // Проверяем, чтобы при старте работало только патрулирование
        if (patrolScript != null) patrolScript.enabled = true;
        if (evasionScript != null) evasionScript.enabled = false;
        if (shootingScript != null) shootingScript.enabled = false;
    }

    private void OnEnable()
    {
        enemyHealthScript.DamageTaken += OnTakeDamage;
    }

    private void OnDisable()
    {
        enemyHealthScript.DamageTaken -= OnTakeDamage;
    }

    void Update()
    {

        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        // Ищем игрока в радиусе видимости
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, visionRadius, playerLayer);

        if (playerCollider != null)
        {
            // Игрок замечен! Переключаем скрипты
            playerSpotted = true;
            ActivateCombatState();
        }
        else
        {
            ActivatePatrolState();
        }
    }

    void ActivateCombatState()
    {
        Debug.Log($"[{gameObject.name}] Заметил игрока! Перехожу в боевой режим.");

        // Отключаем патруль
        if (patrolScript != null) patrolScript.enabled = false;

        // Включаем стрейф и стрельбу
        if (evasionScript != null) evasionScript.enabled = true;
        if (shootingScript != null) shootingScript.enabled = true;
    }

    void ActivatePatrolState()
    {
        Debug.Log($"[{gameObject.name}] Заметил игрока! Перехожу в боевой режим.");

        // Отключаем патруль
        if (patrolScript != null) patrolScript.enabled = true;

        // Включаем стрейф и стрельбу
        if (evasionScript != null) evasionScript.enabled = false;
        if (shootingScript != null) shootingScript.enabled = false;
    }

    // Отрисовка радиуса зрения в редакторе Unity (синий круг)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }

    private void OnTakeDamage()
    {
        ActivateCombatState();
    }
}
