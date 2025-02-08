using UnityEngine;
using UnityEngine.SceneManagement;

public class Boundary : MonoBehaviour
{
    // Flags
    private bool isCollided;

    private void Start()
    {
        // Initialize flags
        isCollided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with the game object, return them to the level's starting position
        if (other.gameObject.CompareTag("Player") && !isCollided)
        {
            isCollided = true;                                              // Update flag to show that the player has collided with the game object

            // Checks the current scene to determine how to reposition player
            if (SceneManager.GetActiveScene().name == "The Forest") ForestManager.fm.blackoutScreen.GetComponent<Animator>().SetBool("FadeIn", true);
            else if (SceneManager.GetActiveScene().name == "The Cabin") CabinManager.cm.blackoutScreen.GetComponent<Animator>().SetBool("FadeIn", true);

            Invoke("ReturnToStart", 3);
        }
    }

    // Returns the player to the level's starting position
    private void ReturnToStart()
    {
        if (SceneManager.GetActiveScene().name == "The Forest")
        {
            ForestManager.fm.player.transform.position = ForestManager.fm.playerSpawnPoint.transform.position;
            ForestManager.fm.player.transform.rotation = ForestManager.fm.playerSpawnPoint.transform.rotation;
            ForestManager.fm.blackoutScreen.GetComponent<Animator>().SetBool("FadeIn", false);
        }
        else if (SceneManager.GetActiveScene().name == "The Cabin")
        {
            CabinManager.cm.player.transform.position = CabinManager.cm.playerSpawnPoint.transform.position;
            CabinManager.cm.player.transform.rotation = CabinManager.cm.playerSpawnPoint.transform.rotation;
            CabinManager.cm.blackoutScreen.GetComponent<Animator>().SetBool("FadeIn", false);
        }
        
        isCollided = false;                                                 // Update flag to show that the player is not colliding with the game object
    }
}