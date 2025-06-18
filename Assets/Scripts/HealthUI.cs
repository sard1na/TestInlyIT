using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public HealthController playerHealth;
    public TextMeshProUGUI healthText;

    void Update()
    {
        if (playerHealth != null)
        {
            healthText.text = "Çהמנמגüו: " + playerHealth.currentHealth;
        }
    }
}
