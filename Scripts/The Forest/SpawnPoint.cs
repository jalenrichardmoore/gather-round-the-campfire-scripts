using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Prefab References
    [SerializeField] private GameObject logPrefab;
    [SerializeField] private GameObject coalPrefab;
    [SerializeField] private GameObject pineconePrefab;

    [HideInInspector] public float timeUntilNextSpawn;                      // Stores time until a new item can be spawned

    // Flags
    public bool itemSpawned;                                                // Flag that checks if an item is currently spawned at this location

    private void Start()
    {
        // Initialize variables
        timeUntilNextSpawn = Random.Range(5f, 15f);
        itemSpawned = false;
    }

    private void Update()
    {
        if (!itemSpawned && Time.time > timeUntilNextSpawn) SpawnItem();    // Check if an item is not spawned and enough time has passed to spawn an object, then spawns an object
    }

    // Spawns an item at the spawn point
    private void SpawnItem()
    {
        GameObject objectToSpawn = null;
        int randomNumber = Random.Range(0, 100);                            // Generate a random number between 0-99

        if (randomNumber < 50) objectToSpawn = pineconePrefab; 
        else if (randomNumber < 80) objectToSpawn = coalPrefab; 
        else if (randomNumber < 100) objectToSpawn = logPrefab;

        Instantiate(objectToSpawn, transform.position, Quaternion.Euler(objectToSpawn.GetComponent<ForestItem>().spawnRotation), transform);
        itemSpawned = true;
    }
}