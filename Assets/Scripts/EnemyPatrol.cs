using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Настройки патруля")]
    [SerializeField] private Transform[] waypoints; // Массив точек маршрута
    [SerializeField] private float speed = 3f;         // Скорость перемещения
    [SerializeField] private float closeEnough = 0.2f; // Дистанция, чтобы засчитать прибытие на точку

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        // Вычисляем целевую точку
        Vector3 targetPos = waypoints[currentWaypointIndex].position;

        // Двигаем врага
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Поворачиваем врага по направлению движения (вдоль оси X)
        Vector3 direction = targetPos - transform.position;
        if (direction.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Если дошли до точки — переключаемся на следующую
        if (Vector2.Distance(transform.position, targetPos) < closeEnough)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

    }
}
