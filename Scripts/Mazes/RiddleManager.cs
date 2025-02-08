using UnityEngine;
using TMPro;
using Invector.vCharacterController;

public class RiddleManager : MonoBehaviour
{
    // Static Reference
    public static RiddleManager rm;

    // UI References
    [SerializeField] private GameObject riddleScreen;
    [SerializeField] private TMP_InputField inputField;

    // Riddle Variables
    [SerializeField] private vThirdPersonInput playerInput;
    [SerializeField] private GameObject [] clues;

    // Flags
    private bool isPanelActive;
    private bool riddleSolved;

    private void Start()
    {
        // Initialize variables
        isPanelActive = false;
        riddleSolved = false;

        // Disable riddle screen
        riddleScreen.SetActive(false);
    }

    private void Update()
    {
        // If the 'E' key or 'ESC' key is pressed, toggle the riddle on/off
        if (Input.GetKeyDown(KeyCode.E) && !isPanelActive && !riddleSolved) TogglePanel(true);
        else if (Input.GetKeyDown(KeyCode.Escape) && isPanelActive) TogglePanel(false);

        // If the 'Enter' key is pressed while the riddle is active, validate the player's answer
        if (isPanelActive && Input.GetKeyDown(KeyCode.Return)) CheckInput();

        // If the player has visited all clues, reset objective counter
        if (clues[clues.Length - 1].GetComponent<RiddleClue>().foundRiddle)
        {
            foreach (GameObject clue in clues) clue.GetComponent<RiddleClue>().foundRiddle = false;
            MazeManager.mm.objectivesCompleted = 0;
            MazeManager.mm.currentObjectiveIndex = 0;
            MazeManager.mm.objectiveComplete = false;
        }
    }

    // Toggles the riddle panel on/off
    private void TogglePanel(bool state)
    {
        isPanelActive = state;
        riddleScreen.SetActive(state);

        if (isPanelActive)                                                  // If the panel is active, enable the cursor
        {
            Cursor.visible = true;
            playerInput.enabled = false;                                    // Disable player movement
            inputField.text = "";
            inputField.ActivateInputField();                                // Activate input field
        }
        else                                                                // If the panel is inactive, disable the cursor
        {
            Cursor.visible = false;
            playerInput.enabled = true;                                     // Activate player input field
        }
    }

    // Checks if player inputs the answer to the riddle
    private void CheckInput()
    {
        string userInput = inputField.text;                                 // Stores player input

        // If the player inputs the correct answer, position them in front of the treasure
        if (userInput.Equals("Friendship", System.StringComparison.OrdinalIgnoreCase))
        {
            riddleSolved = true;
            TogglePanel(false);
            MazeManager.mm.objectivesCompleted = 4;
        }
        else
        {
            TogglePanel(false);
        }
    }
}