using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public InputManager inputManager;        // Скрипт управления
    public float moveSpeed = 5f;             // Текущая скорость
    private float originalSpeed;             // Базовая скорость
    private Coroutine boostCoroutine;        // Ссылка на корутину буста

    public float maxX = 12.5f;
    public float maxZ = 12.5f;
    public float minX = -12.5f;
    public float minZ = -12.5f;

    public TrailRenderer trail;              // ССЫЛКА НА TrailRenderer

    void Start()
    {
        originalSpeed = moveSpeed;

        // Отключаем след при запуске (на всякий случай)
        if (trail != null)
            trail.enabled = false;
    }

    void Update()
    {
        Vector2 input = inputManager.GetInput();
        Vector3 movement = new Vector3(input.x, 0, input.y).normalized;

        Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;
    }

   
    public void BoostSpeed(float newSpeed, float duration)
    {
        if (boostCoroutine != null)
        {
            StopCoroutine(boostCoroutine);
        }

        boostCoroutine = StartCoroutine(SpeedBoostCoroutine(newSpeed, duration));
    }

    
    private IEnumerator SpeedBoostCoroutine(float boostedSpeed, float duration)
    {
        moveSpeed = boostedSpeed;

        if (trail != null)
            trail.enabled = true;

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;

        if (trail != null)
            trail.enabled = false;
    }
}
