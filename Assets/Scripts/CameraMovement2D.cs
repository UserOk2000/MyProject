using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2D : MonoBehaviour
{
    [Header("Ссылки")]
    public Transform target;

    [Header("Настройки плавности")]
    public float smoothTime = 0.3f;

    [Header("Смещение")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    private Vector3 velocity = Vector3.zero;

    // Переменные для тряски
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;

    void LateUpdate()
    {
        if (target == null) return;

        // Базовая позиция следования за игроком
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Если активна тряска, добавляем случайное смещение
        if (shakeDuration > 0)
        {
            Vector2 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            smoothedPosition += new Vector3(shakeOffset.x, shakeOffset.y, 0f);

            // Уменьшаем время тряски с каждым кадром
            shakeDuration -= Time.deltaTime;
        }

        transform.position = smoothedPosition;
    }

    // Метод, который мы будем вызывать из других скриптов
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}

