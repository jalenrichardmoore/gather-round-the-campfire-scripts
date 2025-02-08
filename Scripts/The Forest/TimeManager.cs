using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    // Static Reference
    public static TimeManager tm;

    // Skybox Materials
    [SerializeField] private Texture2D nightSkybox;
    [SerializeField] private Texture2D sunriseSkybox;
    [SerializeField] private Texture2D daySkybox;
    [SerializeField] private Texture2D sunsetSkybox;

    // Lighting Gradients
    [SerializeField] private Gradient nightToSunriseGradient;
    [SerializeField] private Gradient sunriseToDayGradient;
    [SerializeField] private Gradient dayToSunsetGradient;
    [SerializeField] private Gradient sunsetToNightGradient;

    // Lighting References
    [SerializeField] private Light globalLight;

    // Timer Variables
    [SerializeField] private int minutes;
    public int Minutes {get {return minutes;} set {minutes = value; OnMinutesChange(value); }}

    [SerializeField] private int hours;
    public int Hours {get {return hours;} set {hours = value; OnHoursChange(value); }}

    private float tempSecond;

    // Flags
    [HideInInspector] public bool dayOver;
    [SerializeField] private bool endingDay;

    private void Start()
    {
        // Set the current skybox and lighting to be nighttime, and update environment
        DynamicGI.UpdateEnvironment();
        globalLight.transform.rotation = Quaternion.Euler(-90f, 90f, 0f);
        globalLight.color = nightToSunriseGradient.Evaluate(0f);
        globalLight.intensity = 1.2f;

        RenderSettings.skybox.SetTexture("_Texture1", nightSkybox);
        RenderSettings.skybox.SetTexture("_Texture2", nightSkybox);
        DynamicGI.UpdateEnvironment();

        // Initialize variables
        tm = GetComponent<TimeManager>();
        tempSecond = 0;
        Minutes = 0;
        Hours = 0;
        dayOver = false;
        endingDay = false;
    }

    private void Update()
    {
        tempSecond += Time.deltaTime;                                       // Increment seconds every frame
        if (tempSecond >= .125f)                                            // Check if an in-game second has passed
        {
            Minutes++;                                                      // Increment number of minutes passed
            tempSecond = 0;                                                 // Reset second counter
        }

        if (dayOver && !DialogueManager.dm.isSpeaking && !endingDay) 
        {
            endingDay = true;
            ForestManager.fm.blackoutScreen.GetComponent<Animator>().SetBool("FadeIn", true);
            StartCoroutine(AudioManager.am.FadeBGM(AudioManager.am.mazeTheme, 1f));
            Invoke("LoadMaze", 2);
        }
    }

    private void LoadMaze()
    {
        SceneManager.LoadScene(GameData.mazes[GameData.currentMazeIndex]);
    }

    // Increments number of hours every 60 in-game minutes, and transitions the scene after 24 hours
    private void OnMinutesChange(int value)
    {
        if (value >= 60)                                                    // Checks if 60 minutes (1 hour) have passed
        {
            Hours++;                                                        // Increment number of hours passed
            Minutes = 0;                                                    // Reset minute counter
        }

        if (Hours >= 24)                                                    // Checks if 24 hours (1 day) have passed
        {
            dayOver = true;
        }
    }

    // Transitions between skyboxes after passing hour thresholds
    private void OnHoursChange(int value)
    {
        if (value == 6) StartCoroutine(LerpSkybox(nightSkybox, sunriseSkybox, nightToSunriseGradient, 1.4f, Quaternion.Euler(162f, 90f, 0f), 10f));
        else if (value == 8) StartCoroutine(LerpSkybox(sunriseSkybox, daySkybox, sunriseToDayGradient, 1.2f, Quaternion.Euler(90f, 90f, 0f), 10f));
        else if (value == 18) StartCoroutine(LerpSkybox(daySkybox, sunsetSkybox, dayToSunsetGradient, 1.4f, Quaternion.Euler(18f, 90f, 0f), 10f));
        else if (value == 22) StartCoroutine(LerpSkybox(sunsetSkybox, nightSkybox, sunsetToNightGradient, 1.2f, Quaternion.Euler(-90f, 90f, 0f), 10f));
    }

    // Coroutine that lerps between skyboxes, lighting colors, and lighting positions
    private IEnumerator LerpSkybox(Texture2D firstSkybox, Texture2D secondSkybox, Gradient newGradient, float newIntensity, Quaternion newRotation, float lerpTime)
    {
        // Set the new textures to the skybox
        RenderSettings.skybox.SetTexture("_Texture1", firstSkybox);
        RenderSettings.skybox.SetTexture("_Texture2", secondSkybox);
        RenderSettings.skybox.SetFloat("_Blend", 0);

        // Store the global light's current intensity and rotation
        float initialIntensity = globalLight.intensity;
        Quaternion initialRotation = globalLight.transform.rotation;

        for (float i = 0; i < lerpTime; i += Time.deltaTime)                // Transition between skybox settings for 'lerpTime' seconds
        {
            RenderSettings.skybox.SetFloat("_Blend", i / lerpTime);         // Incrementally transition from the current skybox to the next
            globalLight.color = newGradient.Evaluate(i / lerpTime);         // Incrementally transition along light gradient

            globalLight.intensity = Mathf.Lerp(initialIntensity, newIntensity, i / lerpTime);
            globalLight.transform.rotation = Quaternion.Lerp(initialRotation, newRotation, i / lerpTime);
            
            DynamicGI.UpdateEnvironment();                                  // Update the skybox after each increment
            yield return null;
        }

        // Set all values to be the new settings
        RenderSettings.skybox.SetTexture("_Texture1", secondSkybox);
        globalLight.color = newGradient.Evaluate(1);
        globalLight.intensity = newIntensity;
        globalLight.transform.rotation = newRotation;
    }
}