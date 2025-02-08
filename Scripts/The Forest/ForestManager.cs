using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForestManager : MonoBehaviour
{
    // Static Reference
    static public ForestManager fm;

    // UI References
    public GameObject blackoutScreen;
    [SerializeField] private List<TMP_Text> materialCounts;

    // Forest Variables
    public GameObject player;
    public GameObject playerSpawnPoint;
    public GameObject [] campsites;

    public int [] collectedMaterials;

    public float timeUntilNextAmbiance;

    // Flags
    public bool hasCollectedMaterial;
    public bool isPlayingAmbiance;

    private void Awake()
    {
        // Set audio to loop
        AudioManager.am.audioSource.loop = true;
    }

    private void Start()
    {
        // Initialize variables
        fm = GetComponent<ForestManager>();

        GameData.totalTimeAccumulated = 0f;
        timeUntilNextAmbiance = 0f;
        hasCollectedMaterial = false;
        isPlayingAmbiance = false;

        collectedMaterials = new int[3];
        for (int i = 0; i < 3; i++) collectedMaterials[i] = 0;

        foreach (TMP_Text count in materialCounts) count.gameObject.SetActive(false);

        foreach (GameObject campsite in campsites) campsite.SetActive(false);
        for (int i = GameData.currentMazeIndex; i < campsites.Length; i++) campsites[i].SetActive(true);

        // Position player at the level's starting position
        player.transform.GetChild(0).GetComponent<Renderer>().material.SetFloat("_Emission", GameData.playerLight);
        player.transform.position = playerSpawnPoint.transform.position;
    }

    private void Update()
    {
        DisplayMaterialCounts();                                            // Update item tracker UI

        if (!isPlayingAmbiance && Time.time > timeUntilNextAmbiance)        // If no ambiance is playing, play a random ambiance clip and update interval until the next ambiance
        {
            isPlayingAmbiance = true;
            StartCoroutine(AudioManager.am.SelectForestAmbiance());
        }

        if (Input.GetKey(KeyCode.F))                                        // If the 'F' key is pressed, immediately end the day and transition scences
        {
            TimeManager.tm.dayOver = true;
        }
    }

    // Displays the amount of each material collected
    private void DisplayMaterialCounts()
    {
        List<GameObject> countsToDisplay = new List<GameObject>();          // Initialize list of collected materials
        Vector3 position = Vector3.zero;

        // If a material has been collected, add it to the list of item counts to be displayed
        for (int i = 0; i < collectedMaterials.Length; i++)
        {
            if (collectedMaterials[i] > 0)
            {
                countsToDisplay.Add(materialCounts[i].gameObject);
                materialCounts[i].gameObject.SetActive(true);
                materialCounts[i].text  = GameData.materials[i] + " x" + collectedMaterials[i];
            }
            else
            {
                materialCounts[i].gameObject.SetActive(false);
            }
        }

        // Update UI to display each material count
        for (int i = 0; i < countsToDisplay.Count; i++)
        {
            countsToDisplay[i].GetComponent<RectTransform>().anchoredPosition = position;
            position += new Vector3(0, -75f, 0f);
        }
    }

    // Adds extra time to maze timer for each item collected and removes all items from inventory
    public void DepositItems()
    {
        for (int i = 0; i < 3; i++)
        {
            GameData.totalTimeAccumulated += collectedMaterials[i] * 15 * (i + 1);
            collectedMaterials[i] = 0;
        }
    }
}