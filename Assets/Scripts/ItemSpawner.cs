using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; 
    public float spawnInterval = 3f; 
    public float areaSize = 12f; 

    void Start()
    {
        InvokeRepeating(nameof(SpawnItem), 1f, spawnInterval);
    }

    void SpawnItem()
    {
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-areaSize, areaSize), 0.5f, Random.Range(-areaSize, areaSize));
        Instantiate(itemPrefabs[randomIndex], spawnPos, Quaternion.identity);
    }
}
