using UnityEngine;

public class BonusItem : MonoBehaviour
{
    public float boostSpeed = 10f;
    public float boostDuration = 4f;

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
           
            player.BoostSpeed(boostSpeed, boostDuration);

            
            ActionLogManager.Instance.AddEntry($"Собран бонус: ускорение +{boostSpeed} на {boostDuration} сек");

            
            Destroy(gameObject);
        }
    }
}
