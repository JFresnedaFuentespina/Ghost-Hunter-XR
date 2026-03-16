using UnityEngine;
using UnityEngine.AI;

public class GhostBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform playerTransform;
    public float minDistance = 1f;
    public float speed = 2f;
    public NavMeshAgent agent;
    void Start()
    {
        FindPlayer();

        agent.enabled = true;

        // Comprobar si el agente está sobre el NavMesh
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            agent.SetDestination(playerTransform.position);
        }

        agent.stoppingDistance = minDistance;
        agent.speed = speed;
    }
    void Update()
    {
        if (playerTransform != null && agent != null)
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }
}
