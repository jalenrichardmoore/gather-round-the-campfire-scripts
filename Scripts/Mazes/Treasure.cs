using UnityEngine;

public class Treasure : MonoBehaviour
{
    // Treasure Variables
    [SerializeField] private Dialogue finalDialogue;
    private Vector3 startPosition;

    // Flags
    private bool interacted;

    private void Start()
    {
        // Initialize variables
        startPosition = transform.position;

        // Initialize flags
        interacted = false;
    }

    private void Update()
    {
        // Rotate the game object around the global y-axis
        transform.Rotate(0f, 360f * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerStay(Collider other)
    {
        // If the 'E' key is pressed while the player collides with the game object, begin displaying the maze's final dialogue
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E) && !interacted)
        {
            interacted = true;
            StartCoroutine(AudioManager.am.FadeBGM(AudioManager.am.treasure, .5f));
            MazeManager.mm.displayTime = false;
            GameData.currentMazeIndex++;                                    // Increment number of mazes completed
            GameData.playerLight += 2f;                                     // Increase player's light intensity
            MazeManager.mm.treasureCollected = true;                        // Update flag to show that the maze's treasure has been collected
            DialogueManager.dm.StartDialogue(finalDialogue);
        }
    }
}