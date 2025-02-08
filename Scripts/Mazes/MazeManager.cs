using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MazeManager : MonoBehaviour
{
    // Static Reference
    static public MazeManager mm;

    // UI References
    public GameObject blackOutScreen;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text objectiveText;

    // Maze Variables
    public GameObject player;
    public GameObject treasureRoomBarrier;
    public GameObject treasureRoomDoor;
    public GameObject playerSpawnPoint;
    public GameObject [] objectiveSpawnPoints;

    [SerializeField] private KeyCode cheatCode;

    [SerializeField] private string mazeObjective;
    [SerializeField] private string avatarObjective;

    public int currentObjectiveIndex;
    public int objectivesCompleted;
    [SerializeField] private int maxObjectives;

    private float timeRemaining;

    // Flags
    public bool dialogueFinished;
    public bool treasureCollected;
    public bool displayTime;
    public bool objectiveComplete;

    [SerializeField] private bool warpToTreasure;
    private bool endingMaze;

    private void Start()
    {
        // Initialize variables
        mm = GetComponent<MazeManager>();
        timeRemaining = 60f + GameData.totalTimeAccumulated;
        currentObjectiveIndex = 0;

        // Initialize flags
        dialogueFinished = false;
        treasureCollected = false;
        displayTime = true;
        objectiveComplete = false;
        warpToTreasure = false;
        endingMaze = false;

        // If there is a door guarding the treasure, enable it
        if (treasureRoomBarrier != null)
        {
            treasureRoomDoor.SetActive(false);
            treasureRoomBarrier.SetActive(true);
        }
        else treasureRoomDoor.SetActive(true);

        // Position the player at the beginning of the maze
        player.transform.GetChild(0).GetComponent<Renderer>().material.SetFloat("_Emission", GameData.playerLight);
        player.transform.position = playerSpawnPoint.transform.position;
        player.transform.rotation = playerSpawnPoint.transform.rotation;
    }

    private void Update()
    {
        // If time has run out or the player has collected the treasure, end the level and load the appropriate level
        if ((timeRemaining <= 0f || dialogueFinished) && !endingMaze)
        {
            endingMaze = true;
            blackOutScreen.GetComponent<Animator>().SetBool("FadeIn", true);
            
            // If the player has completed the last maze, load the final level
            if (dialogueFinished && SceneManager.GetActiveScene().buildIndex == 6)
            {
                StartCoroutine(AudioManager.am.FadeBGM(AudioManager.am.cabinTheme, .5f));
                Invoke("LoadCabin", 1);
            }
            else
            {
                StartCoroutine(AudioManager.am.FadeBGM(AudioManager.am.forestThemes[Random.Range(0, AudioManager.am.forestThemes.Length)], .5f));
                Invoke("LoadForest", 1);

            }
        }

        // Display the current time remaining in the maze and update timer
        if (displayTime) DisplayTime(timeRemaining);
        if (!treasureCollected) timeRemaining = Mathf.Max(timeRemaining - Time.deltaTime, 0f);

        // Update player objective UI
        if (SceneManager.GetActiveScene().buildIndex == 5 && !objectiveComplete) objectiveText.text = mazeObjective; 
        else if (treasureRoomBarrier != null && !objectiveComplete) objectiveText.text = mazeObjective + "\n" + objectivesCompleted + "/" + maxObjectives;
        else objectiveText.text = avatarObjective;
        
        // If the player has completed all maze objectives, position them at the treasure
        if (maxObjectives > 0 && objectivesCompleted == maxObjectives && !warpToTreasure)
        {
            warpToTreasure = true;
            blackOutScreen.GetComponent<Animator>().SetBool("FadeIn", true);
            if (treasureRoomBarrier != null) treasureRoomBarrier.SetActive(false);
            treasureRoomDoor.SetActive(true);
            Invoke("WarpToTreasure", 1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))                               // If the 'ESC' key is pressed, position the player at the start of the maze
        {
            blackOutScreen.GetComponent<Animator>().SetBool("FadeIn", true);
            Invoke("WarpToStart", 1);
        }

        if (Input.GetKeyDown(cheatCode))                                    // If the player uses the maze's cheat code, position the player at the next objective
        {
            blackOutScreen.GetComponent<Animator>().SetBool("FadeIn", true);
            Invoke("WarpToObjective", 1);
        }
    }
    
    // Displays the timer text in a minutes-seconds format
    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);               // Calculate total minutes left
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);               // Calculate total seconds left

        // Display the time in a minutes-seconds format
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Loads 'The Forest' level
    private void LoadForest()
    {
        SceneManager.LoadScene("The Forest");
    }

    // Loads 'THe Cabin' level
    private void LoadCabin()
    {
        SceneManager.LoadScene("The Cabin");
    }

    // Positions the player at the start of the maze
    private void WarpToStart()
    {
        player.transform.position = playerSpawnPoint.transform.position;
        player.transform.rotation = playerSpawnPoint.transform.rotation;
        blackOutScreen.GetComponent<Animator>().SetBool("FadeIn", false);
    }

    // Positions the player in front of a maze objective
    private void WarpToObjective()
    {
        player.transform.position = objectiveSpawnPoints[currentObjectiveIndex % objectiveSpawnPoints.Length].transform.position;
        player.transform.rotation = objectiveSpawnPoints[currentObjectiveIndex % objectiveSpawnPoints.Length].transform.rotation;
        currentObjectiveIndex++;
        blackOutScreen.GetComponent<Animator>().SetBool("FadeIn", false);
    }

    // Positions the player in front of the treasure room
    private void WarpToTreasure()
    {
        player.transform.position = objectiveSpawnPoints[objectiveSpawnPoints.Length - 1].transform.position;
        player.transform.rotation = objectiveSpawnPoints[objectiveSpawnPoints.Length - 1].transform.rotation;
        blackOutScreen.GetComponent<Animator>().SetBool("FadeIn", false);
        objectiveComplete = true;
    }
}