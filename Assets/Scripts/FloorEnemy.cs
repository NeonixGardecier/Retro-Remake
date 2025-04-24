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
    }

    void Update()
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
