using UnityEngine;

public class LonelyAvatar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with the avatar, destroy the avatar and update objective counter
        if (other.CompareTag("Player"))
        {
            AudioManager.am.audioSource.PlayOneShot(AudioManager.am.avatar);
            transform.parent.GetComponent<Animator>().SetBool("FadeAway", true);

            transform.parent.parent.GetComponent<AvatarSpawnPoint>().timeUntilNextSpawn = Time.time + Random.Range(10f, 15f);
            MazeManager.mm.objectivesCompleted++;
            Destroy(gameObject, 1f);
        }
    }
}