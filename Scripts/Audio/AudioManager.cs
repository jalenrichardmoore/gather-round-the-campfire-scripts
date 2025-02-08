using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Static Reference
    public static AudioManager am;
    
    // Audio Reference
    public AudioSource audioSource;

    [SerializeField] private AudioClip [] forestAmbianceClips;
    public AudioClip [] forestThemes;
    public AudioClip [] avatarVoices;
    public AudioClip mazeTheme;
    public AudioClip cabinTheme;
    public AudioClip interact;
    public AudioClip treasure;
    public AudioClip passion;
    public AudioClip avatar;

    private void Awake()
    {
        // Ensures only there is only one Audio Manager in the scene
        if (am != null && am != this) Destroy(this);
        else am = GetComponent<AudioManager>();

        // Let the Audio Manager persist between scenes
        DontDestroyOnLoad(gameObject);
    }

    // Selects a random audio clip to play during 'The Forest' level
    public void SelectForestTheme()
    {
        audioSource.clip = forestThemes[Random.Range(0, forestThemes.Length)];
    }

    // Selects a random ambiance clip to play during 'The Forest' level
    public IEnumerator SelectForestAmbiance()
    {
        AudioClip ambiance = forestAmbianceClips[Random.Range(0, forestAmbianceClips.Length)];
        audioSource.PlayOneShot(ambiance);
        yield return new WaitForSeconds(ambiance.length);

        ForestManager.fm.timeUntilNextAmbiance = Time.time + 7f;
        ForestManager.fm.isPlayingAmbiance = false;
    }

    // Switches background music audio clips
    public IEnumerator FadeBGM(AudioClip newClip, float duration)
    {
        float startVolume = audioSource.volume;

        // Fade out the original background music
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, i / duration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 0;

        // Fade in the new background music
        audioSource.clip = newClip;
        audioSource.Play();

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, i / duration);
            yield return null;
        }

        audioSource.volume = startVolume;
    }
}