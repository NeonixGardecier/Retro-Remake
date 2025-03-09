using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    public Collider playerCollider;   
    public Transform playerPos; 
    public Vector3 offset;

    void Update()
    {
        transform.position = playerPos.position + offset;
    }    
}
