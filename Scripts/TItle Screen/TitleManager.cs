using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Static Reference
    static public TitleManager tm;

    // UI References
    [SerializeField] private GameObject blackoutScreen;

    // Title Variables
    [SerializeField] private Dialogue titleScreenDialogue;

    // Flags
    public bool dialogueFinished;
    public bool startDialogue;
    public bool sceneLoaded;

    private void Start()
    {
        // Initialize variables
        tm = GetComponent<TitleManager>();
        dialogueFinished = false;
        startDialogue = false;
        sceneLoaded = false;
    
        // Stops all audio if audio is playing
        AudioManager.am.audioSource.Stop();
    }

    private void Update()
    {
        if (startDialogue)                                                      // If the game has started, begin displaying the opening dialogue
        {
            startDialogue = false;
            Invoke("OpeningDialogue", 2);
        }

        if (dialogueFinished && !sceneLoaded)                                   // If the opening dialogue is finished, load 'The Forest' level
        {
            sceneLoaded = true;
            StartCoroutine(AudioManager.am.FadeBGM(AudioManager.am.forestThemes[Random.Range(0, AudioManager.am.forestThemes.Length)], 0.5f));
            Invoke("LoadForest", 1);
        }
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Resets all game data and begins displaying the starting dialogue
    public void StartGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameData.ResetGame();
        blackoutScreen.GetComponent<Animator>().SetBool("FadeIn", true);
        startDialogue = true;
    }

    // Begins the coroutine to display the starting dialogue
    private void OpeningDialogue()
    {
        DialogueManager.dm.StartDialogue(titleScreenDialogue);
    }

    // Loads 'The Forest' level
    private void LoadForest()
    {
        SceneManager.LoadScene("The Forest");
    }
}