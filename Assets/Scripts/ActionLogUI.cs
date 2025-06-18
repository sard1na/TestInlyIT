using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ActionLogUI : MonoBehaviour
{
    public GameObject logPanel; // LogPanel
    public Transform contentParent; // Content внутри ScrollView
    public GameObject logTextPrefab; // Prefab дл€ строки текста (TMP_Text)

    void Start()
    {
        logPanel.SetActive(false); // —крываем по умолчанию
    }

    public void ToggleLogPanel()
    {
        bool isActive = logPanel.activeSelf;
        logPanel.SetActive(!isActive);

        if (!isActive)
        {
            LoadLog();
        }
    }

    public void CloseLogPanel()
    {
        logPanel.SetActive(false);
    }

    private void LoadLog()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        List<string> entries = ActionLogManager.Instance.GetEntries();

        foreach (string entry in entries)
        {
            GameObject item = Instantiate(logTextPrefab, contentParent);
            item.GetComponent<TMP_Text>().text = entry;
        }
    }
}
