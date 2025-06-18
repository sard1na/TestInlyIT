using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public HealthController healthComponent; // ������ �� ��������� �������� ������

    private float originalSpeed;  // UI �������� ��� ����������� ������� � �������:


    public GameObject notePanel;   // ������ ��� ������ ������ �������

    public TMP_Text noteTextUI;        // ����� ������ ������ ������

    public TMP_Text historyText;       // ����� ��� ������� �������

    private string historyLog = ""; // ������ ��� �������� �������

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ��������������, ��� ���� ��������� PlayerController �� ��������� speed:
        var controller = GetComponent<PlayerController>();
        if (controller != null)
            originalSpeed = controller.moveSpeed;

        if (notePanel != null)
            notePanel.SetActive(false); // �������� ������ ��� ������

        LoadHistory(); // ��������� ������� �� ����� ��� �������
    }

    /// <summary>
    /// ����� ��������� ���� � ��������.
    /// </summary>
    /// <param name="damage">���������� �����</param>
    public void ApplyDamage(int damage)
    {
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
            AddToHistory($"������� ����: {damage}");

            if (healthComponent.currentHealth <= 0)
            {
                Debug.Log("����� ����");
                // ����� �������� ���������� ������ ��� ������ ������ ������.
            }
        }
        else
        {
            Debug.LogWarning("Health ��������� �� ��������");
        }
    }

    /// <summary>
    /// ����� ��������� ��������� ����� ��������.
    /// </summary>
    /// <param name="multiplier">��������� ��������</param>
    /// <param name="duration">������������ � ��������</param>
    public void ApplyBonus(float multiplier, float duration)
    {
        StartCoroutine(BonusCoroutine(multiplier, duration));
        AddToHistory($"������� ����� �������� x{multiplier} �� {duration} ���");
    }

    private IEnumerator BonusCoroutine(float multiplier, float duration)
    {
        var controller = GetComponent<PlayerController>();
        if (controller != null)
            controller.moveSpeed *= multiplier;

        yield return new WaitForSeconds(duration);

        if (controller != null)
            controller.moveSpeed /= multiplier;

        AddToHistory($"����� �������� ��������");
    }

    /// <summary>
    /// ���������� ������� �� ������.
    /// </summary>
    /// <param name="text">����� �������</param>
    public void ShowNote(string text)
    {
        if (notePanel != null && noteTextUI != null)
        {
            notePanel.SetActive(true);
            noteTextUI.text = text;
            AddToHistory($"�������� �������: {text}");
        }
    }

    /// <summary>
    /// ��������� ������ � �������� �� ������.
    /// </summary>
    public void CloseNote()
    {
        if (notePanel != null)
            notePanel.SetActive(false);
    }

    /// <summary>
    /// ��������� ������ � ������� �������.
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