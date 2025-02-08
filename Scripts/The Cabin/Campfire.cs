using UnityEngine;

public class Campfire : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with the game object, end the game
        if (other.CompareTag("Player"))
        {
            CabinManager.cm.EndGame();
        }
    }
}