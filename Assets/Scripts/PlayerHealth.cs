using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Настройки здоровья")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Настройки тряски камеры")]
    [SerializeField] private CameraMovement2D cameraMovement;
    [SerializeField] private float damageShakeDuration = 0.2f;  // Длительность тряски
    [SerializeField] private float damageShakeMagnitude = 0.3f; // Сила тряски

    void Start()
    {
        currentHealth = maxHealth;

        // Если забыли перетащить камеру в инспекторе, пытаемся найти её автоматически
        if (cameraMovement == null)
        {
            cameraMovement = Camera.main.GetComponent<CameraMovement2D>();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Игрок получил урон: {damageAmount}. Текущее здоровье: {currentHealth}");

        // Вызываем тряску камеры при получении урона
        if (cameraMovement != null)
        {
            cameraMovement.Shake(damageShakeDuration, damageShakeMagnitude);
        }

        if (currentHealth <= 0)
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
