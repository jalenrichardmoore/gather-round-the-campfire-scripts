using UnityEngine;

public class RiddleClue : MonoBehaviour
{
    // Riddle Clue Variables
    public Camera targetCamera;

    // Flags
    public bool foundRiddle = false;

    private void Update()
    {
        // Rotate the clue to always face the player
        transform.GetChild(0).LookAt(targetCamera.transform);
        transform.GetChild(0).rotation = Quaternion.LookRotation(-transform.GetChild(0).forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with the clue, update the objective counter
        if (other.CompareTag("Player") && !foundRiddle)
        {
            foundRiddle = true;
            MazeManager.mm.currentObjectiveIndex++;
        }
    }
}