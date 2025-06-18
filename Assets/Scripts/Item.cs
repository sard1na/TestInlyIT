using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Damage, Bonus, Note }
    public ItemType itemType; 
    public string noteText; 
}
