using UnityEngine;

public class ForestItem : MonoBehaviour
{
    // Item Variables
    public Vector3 spawnRotation;

    public int materialID;

    private void OnTriggerStay(Collider other)
    {
        // If the 'E' key is pressed, destroy the game object, update item counter, and tell spawn point to instantiate a new item
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            AudioManager.am.audioSource.PlayOneShot(AudioManager.am.interact);

            // Update spawn point to instantiate a new item after a random interval
            SpawnPoint parentSpawnPoint = GetComponentInParent<SpawnPoint>();
            parentSpawnPoint.timeUntilNextSpawn = Time.time + Random.Range(5f, 15f);
            parentSpawnPoint.itemSpawned = false;

            // Update item counter and flag to show a new item has been collected
            ForestManager.fm.collectedMaterials[materialID] = Mathf.Min(ForestManager.fm.collectedMaterials[materialID] + 1, 7);
            ForestManager.fm.hasCollectedMaterial = true;
            Destroy(gameObject);
        }
    }
}