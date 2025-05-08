using UnityEngine;

public class PowerUps : MonoBehaviour
public bool isPowerUpActive = false;


{
    public float Duration = 5f;
   public Vector2 StartingVelocity;
   public bool isPowerUpActive = false;
     public float Duration = 5f;

   void start()
    {
        GetComponent<RigidBody2D>.Velocitiy = StartingVelocity
    }
    void OnTriggerEnter2D(Collider2D col)

    { if (other.CompareTag("Player"))
        
        
        is PowerUpActive = !isPowerUpActive;

        if (isPowerUpActive)
        {
            powerupRoutine = StartCoroutine(ApplyPowerUp(other.gameObject));
           {
                if (powerUpRoutine != null)
                stopCroutine(powerUpRoutine);
            }
        ActivatePowerup();

    }

    void ActivatePowerup()
    {
        
        Destroy(gameObject);
    }

   private void OnCollisionEnter2D(Collision2D collision)
    {
       var enemy = collision.collider.GetComponent<Enemy>();
    }
}

}