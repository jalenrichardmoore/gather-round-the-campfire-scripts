using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarSpawnPoint : MonoBehaviour
{
    // Avatar Variables
    [SerializeField] private GameObject avatarPrefab;
    public GameObject avatar;

    // Spawn Point Variables
    [HideInInspector] public float timeUntilNextSpawn;

    // Flags
    public bool avatarSpawned;

    private void Start()
    {
        // Initialize variables
        timeUntilNextSpawn = Random.Range(5f, 15f);
        avatarSpawned = false;
        avatar = null;
    }

    private void Update()
    {
        // If the level is 'The Orange Maze' rotate the spawn point around the global y-axis
        if (SceneManager.GetActiveScene().buildIndex == 4) transform.Rotate(-Vector3.up, Space.World);
        
        // If it is time to spawn an avatar and no avatar is currently spawned, spawn an avatar
        if (!avatarSpawned && Time.time > timeUntilNextSpawn && avatarPrefab != null)
        {
            avatar = Instantiate(avatarPrefab, transform.position + Vector3.right, Quaternion.identity, transform);
            
            // Update flag to show that an avatar is spawned at this point
            avatarSpawned = true;
        }
    }
}