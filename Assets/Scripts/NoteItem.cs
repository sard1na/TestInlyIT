using UnityEngine;

public class NoteItem : MonoBehaviour
{
    public string noteText = "Это текст записки";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NoteManager manager = FindObjectOfType<NoteManager>();
            if (manager != null)
            {
                manager.ShowNote(noteText);
                ActionLogManager.Instance.AddEntry("Подобрана записка: '" + noteText + "'");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("NoteManager не найден на сцене.");
            }
        }
    }
}
