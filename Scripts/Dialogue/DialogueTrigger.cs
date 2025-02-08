using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Dialogue Variable
    public Dialogue dialogue;

    // Flags
    private bool dialogueCalled;

    private void Start()
    {
        // Initialize variable
        dialogueCalled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with the trigger, display its dialogue and update flag to show the dialogue has already been called
        if (other.CompareTag("Player") && !dialogueCalled && !MazeManager.mm.treasureCollected && !DialogueManager.dm.isSpeaking)
        {
            dialogueCalled = true;
            DialogueManager.dm.StartDialogue(dialogue);
        }
    }
}