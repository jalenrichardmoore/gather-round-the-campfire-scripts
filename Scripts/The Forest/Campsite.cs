using System.Collections.Generic;
using UnityEngine;

public class Campsite : MonoBehaviour
{
    // Campsite Variables
    public List<Dialogue> dialogueOptions;
    private List<Dialogue> usedDialogueOptions;

    private void Start()
    {
        // Initialize variables
        usedDialogueOptions = new List<Dialogue>();
    }

    private void OnTriggerStay(Collider other) 
    {
        // If the 'E' key is pressed and the player has collected materials while no one is speaking, display a random dialogue option
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E) && ForestManager.fm.hasCollectedMaterial && !DialogueManager.dm.isSpeaking)
        {
            // Play the appropriate voice clip for the hiker
            for (int i = 0; i < ForestManager.fm.campsites.Length; i++)
            {
                if (gameObject == ForestManager.fm.campsites[i])
                {
                    AudioManager.am.audioSource.PlayOneShot(AudioManager.am.avatarVoices[i]);
                    break;
                }
            }

            // Deposit all collected materials and update flag to show the player has no materials
            ForestManager.fm.DepositItems();
            ForestManager.fm.hasCollectedMaterial = false;

            if (dialogueOptions.Count == 0)                                 // If all dialogue options have been exhausted, reset the list
            {
                foreach (Dialogue option in usedDialogueOptions) dialogueOptions.Add(option);
            }

            // Select and display a random dialogue option, removing it from the list of available options
            int dialogueOption = Random.Range(0, dialogueOptions.Count);
            DialogueManager.dm.StartDialogue(dialogueOptions[dialogueOption]);

            usedDialogueOptions.Add(dialogueOptions[dialogueOption]);
            dialogueOptions.Remove(dialogueOptions[dialogueOption]);
        }
    }
}