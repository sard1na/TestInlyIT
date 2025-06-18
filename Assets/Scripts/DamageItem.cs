using UnityEngine;

public class DamageItem : MonoBehaviour
{
    public int damageAmount = 20;

   
    public GameObject damageSparksPrefab;

    void OnTriggerEnter(Collider other)
    {
        HealthController health = other.GetComponent<HealthController>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);

            // Записываем в журнал
            ActionLogManager.Instance.AddEntry($"Получен урон: -{damageAmount} HP");

            
            if (damageSparksPrefab != null)
            {
                Instantiate(damageSparksPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
