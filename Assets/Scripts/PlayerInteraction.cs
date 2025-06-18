using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public HealthController healthComponent; // Ссылка на компонент здоровья игрока

    private float originalSpeed;  // UI элементы для отображения записки и истории:


    public GameObject notePanel;   // Панель для записи текста записки

    public TMP_Text noteTextUI;        // Текст внутри панели записи

    public TMP_Text historyText;       // Текст для истории событий

    private string historyLog = ""; // Строка для хранения истории

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Предполагается, что есть компонент PlayerController со свойством speed:
        var controller = GetComponent<PlayerController>();
        if (controller != null)
            originalSpeed = controller.moveSpeed;

        if (notePanel != null)
            notePanel.SetActive(false); // скрываем панель при старте

        LoadHistory(); // Загружаем историю из файла при запуске
    }

    /// <summary>
    /// Метод применяет урон к здоровью.
    /// </summary>
    /// <param name="damage">Количество урона</param>
    public void ApplyDamage(int damage)
    {
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
            AddToHistory($"Получен урон: {damage}");

            if (healthComponent.currentHealth <= 0)
            {
                Debug.Log("Игрок умер");
                // Можно добавить перезапуск уровня или другую логику смерти.
            }
        }
        else
        {
            Debug.LogWarning("Health компонент не назначен");
        }
    }

    /// <summary>
    /// Метод применяет временный бонус скорости.
    /// </summary>
    /// <param name="multiplier">Множитель скорости</param>
    /// <param name="duration">Длительность в секундах</param>
    public void ApplyBonus(float multiplier, float duration)
    {
        StartCoroutine(BonusCoroutine(multiplier, duration));
        AddToHistory($"Получен бонус скорости x{multiplier} на {duration} сек");
    }

    private IEnumerator BonusCoroutine(float multiplier, float duration)
    {
        var controller = GetComponent<PlayerController>();
        if (controller != null)
            controller.moveSpeed *= multiplier;

        yield return new WaitForSeconds(duration);

        if (controller != null)
            controller.moveSpeed /= multiplier;

        AddToHistory($"Бонус скорости завершен");
    }

    /// <summary>
    /// Отображает записку на экране.
    /// </summary>
    /// <param name="text">Текст записки</param>
    public void ShowNote(string text)
    {
        if (notePanel != null && noteTextUI != null)
        {
            notePanel.SetActive(true);
            noteTextUI.text = text;
            AddToHistory($"Показана записка: {text}");
        }
    }

    /// <summary>
    /// Закрывает панель с запиской по кнопке.
    /// </summary>
    public void CloseNote()
    {
        if (notePanel != null)
            notePanel.SetActive(false);
    }

    /// <summary>
    /// Добавляет запись в историю событий.
    /// </summary>
    private void AddToHistory(string entry)
    {
        string timestampedEntry = $"{System.DateTime.Now.ToString("HH:mm:ss")}: {entry}";
        historyLog += timestampedEntry + "\n";
        if (historyText != null)
            historyText.text = historyLog;

        SaveHistory();
    }

  
    private void SaveHistory()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/history.txt", historyLog);
    }

    
    public void LoadHistory()
    {
        string path = Application.persistentDataPath + "/history.txt";
        if (System.IO.File.Exists(path))
        {
            historyLog = System.IO.File.ReadAllText(path);
            if (historyText != null)
                historyText.text = historyLog;
        }
    }
}