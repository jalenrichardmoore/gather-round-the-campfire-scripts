using UnityEngine;
using Invector.vCharacterController;

public class CabinManager : MonoBehaviour
{
    // Static Reference
    static public CabinManager cm;

    // UI References
    public GameObject blackoutScreen;
    [SerializeField] private GameObject fadeoutScreen;

    // Cabin References
    public GameObject player;
    public GameObject playerSpawnPoint;
    [SerializeField] private vThirdPersonInput playerInput;
    [SerializeField] private Rigidbody playerRB;

    private void Start()
    {
        // Initialize variables
        cm = GetComponent<CabinManager>();

        // Move the player to the initial position and rotation
        player.transform.GetChild(0).GetComponent<Renderer>().material.SetFloat("_Emission", GameData.playerLight);
        player.transform.position = playerSpawnPoint.transform.position;
        player.transform.rotation = playerSpawnPoint.transform.rotation;
    }

    // Displays the game's ending screen
    public void EndGame()
    {
        playerInput.enabled = false;                                        // Disable player movement
        playerRB.useGravity = false;                                        // Disable gravity
        playerRB.velocity = Vector3.zero;                                   // Stop the player's movement
    
        fadeoutScreen.SetActive(true);                                      // Enable the ending game
        Cursor.visible = true;                                              // Enable the cursor
        Cursor.lockState = CursorLockMode.None;
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}