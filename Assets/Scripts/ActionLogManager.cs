using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ActionLogManager : MonoBehaviour
{
    public static ActionLogManager Instance;

    private List<string> logEntries = new List<string>();
    private string logFilePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            logFilePath = Path.Combine(Application.persistentDataPath, "action_log.txt");

            if (File.Exists(logFilePath))
            {
                logEntries = new List<string>(File.ReadAllLines(logFilePath));
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEntry(string entry)
    {
        string timestamped = System.DateTime.Now.ToString("HH:mm:ss") + " — " + entry;
        logEntries.Add(timestamped);
        File.AppendAllLines(logFilePath, new[] { timestamped });

        Debug.Log("Добавлено в журнал: " + timestamped);
    }

    public List<string> GetEntries()
    {
        return logEntries;
    }
}
