using UnityEngine;

public class Passion : MonoBehaviour
{
    // Flags
    private bool collected = false;

    private void OnTriggerStay(Collider other)
    {
        // If the player collides with the object, disable it and update the maze objective counter
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E) && !collected)
        {
            collected = true;
            AudioManager.am.audioSource.PlayOneShot(AudioManager.am.passion);
            MazeManager.mm.currentObjectiveIndex++;
            MazeManager.mm.objectivesCompleted++;

            // Disable the game object and its children
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<ParticleSystem>().Stop();
            }
            GetComponent<ParticleSystem>().Stop();
        }
    }
}