using UnityEngine;
using UnityEngine.AI;

public class PredatorAvatar : MonoBehaviour
{
    // Predator Variables
    [SerializeField] private NavMeshAgent navMeshAgent;

    private Vector3 agentDestination;
    private Vector3 startingPoint;

    private void Start()
    {
        // Initialize variables
        startingPoint = transform.position;
        agentDestination = startingPoint;
        navMeshAgent.SetDestination(agentDestination);
    }

    private void Update()
    {
        // If the player is close to the agent, move the agent towards the player
        if (Vector3.Distance(transform.position, MazeManager.mm.player.transform.position) < 30) agentDestination = MazeManager.mm.player.transform.position;
        else agentDestination = startingPoint;

        navMeshAgent.SetDestination(agentDestination);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with the agent, position the player at the start of the maze
        AudioManager.am.audioSource.PlayOneShot(AudioManager.am.avatar);
        MazeManager.mm.blackOutScreen.GetComponent<Animator>().SetBool("FadeIn", true);
        Invoke("ReturnToStart", 1f);
    }

    // Positions the player at the start of the maze and destroys the predator
    private void ReturnToStart()
    {
        MazeManager.mm.player.transform.position = MazeManager.mm.playerSpawnPoint.transform.position;
        MazeManager.mm.player.transform.rotation = MazeManager.mm.playerSpawnPoint.transform.rotation;
        MazeManager.mm.blackOutScreen.GetComponent<Animator>().SetBool("FadeIn", false);
        Destroy(gameObject, 1f);
    }
}