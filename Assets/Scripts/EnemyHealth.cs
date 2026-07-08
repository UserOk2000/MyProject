using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Настройки здоровья")]
    public int maxHealth = 3; 
    private int currentHealth;

    private void Start()
    {
        
        currentHealth = maxHealth;
    }

    
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Враг получил {damageAmount} урона. Осталось здоровья: {currentHealth}");

       
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Враг уничтожен!");
        Destroy(gameObject); 
    }
}
