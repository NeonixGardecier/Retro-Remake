using UnityEngine;

public class EnemageDamage : MonoBehaviour
{
   public Health playerHealth;

    public int damage = 2;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) //functions will enter once something enters the enemys collider
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(2); //gets the players health scrip 

        }
    }
}
