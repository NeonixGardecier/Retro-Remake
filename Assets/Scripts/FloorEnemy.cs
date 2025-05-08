using UnityEngine;
using UnityEngine.AI;

public class FloorEnemy : MonoBehaviour
{
    public float aggroRange;
    public float minDistance;
    public Transform Player;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player").transform;
    }

    void Update() //If player is in range move towards
    {
        if (Vector3.Distance(transform.position, Player.position) < aggroRange && Vector3.Distance(transform.position, Player.position) > minDistance)
        {
            agent.destination = Player.position;
        }
        else{
            agent.destination = transform.position;
        }
    }
}
