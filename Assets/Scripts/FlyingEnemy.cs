using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    public float direction;
    public float maxLimit;
    public float lowerLimit;

    public float aggroRange;
    public Transform Player;

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Player.position) < aggroRange)
        {
            transform.position = transform.position + new Vector3(-0.05f, direction, 0);
            if(transform.position.y >= maxLimit) direction = -0.05f;
            if(transform.position.y <= lowerLimit) direction = 0.05f;
        }
    }
    
}
