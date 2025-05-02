using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        ActivatePowerup();
    }

    void ActivatePowerup()
    {
        
        Destroy(gameObject);
    }
}
