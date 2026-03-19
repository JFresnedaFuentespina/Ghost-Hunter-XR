using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float minDistance = 1f;
    public float speed = 2f;
    public NavMeshAgent agent;
    public Animator animator;
    public List<GameObject> orbs = new List<GameObject>();
    public GameObject closestOrb;
    void Start()
    {
    }
    void Update()
    {
        FindClosestOrb();
        if (closestOrb)
        {
            agent.SetDestination(closestOrb.transform.position);
            agent.speed = speed;
        }
    }

    public void Kill()
    {
        agent.isStopped = true;
        animator.SetTrigger("Death");
        Destroy(gameObject, 1.5f);
    }

    void FindClosestOrb()
    {

        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        orbs = OrbSpawner.instance.spawnedOrbs;

        foreach (GameObject orb in orbs)
        {
            Vector3 ghostPos = transform.position;
            ghostPos.y = 0;

            Vector3 orbPos = orb.transform.position;
            orbPos.y = 0;

            float d = Vector3.Distance(ghostPos, orbPos);

            if (d < minDistance)
            {
                minDistance = d;
                closest = orb;
            }
        }
        closestOrb = closest;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Orb>())
        {
            Debug.Log("COLISIÓN CON ORBE!");
            OrbSpawner.instance.DestroyOrb(collision.gameObject);
        }
    }
}
