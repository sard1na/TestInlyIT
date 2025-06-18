[System.Serializable]
public class ActionLogEntry
{
    public string timestamp;
    public string description;

    public ActionLogEntry(string desc)
    {
        timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        description = desc;
    }
}
