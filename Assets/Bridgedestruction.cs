using UnityEngine;

public class BridgeDestruction : MonoBehaviour
{
    public GameObject bridge; 
    public GameObject bridge1;
    public float delay = 2f; 
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            Destroy(bridge, delay); 
            Destroy(gameObject); 
        }
    }
}

//chatgpt fixed syntax errors 