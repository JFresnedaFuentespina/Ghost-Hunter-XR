using UnityEngine;
using UnityEngine.AI;

public class GhostBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float minDistance = 1f;
    public float speed = 2f;
    public NavMeshAgent agent;
    void Start()
    {
    }
    void Update()
    {
        Vector3 targetPosition = Camera.main.transform.position;
        agent.SetDestination(targetPosition);
        agent.speed = speed;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
