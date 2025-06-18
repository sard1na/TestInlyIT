using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 100;
    [HideInInspector]
    public int currentHealth;

    public GameObject deathScreen; 

    void Start()
    {
        currentHealth = maxHealth;
        if (deathScreen != null)
            deathScreen.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Здоровье: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Игрок погиб");

        if (deathScreen != null)
        {
            deathScreen.SetActive(true); 
        }

        Time.timeScale = 0f; 
    }
}
