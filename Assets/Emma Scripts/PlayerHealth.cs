using UnityEngine;

public class Health : MonoBehaviour
{

    public int health;
    public int maxHealth = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth; //players max health 

    }

   public void TakeDamage(int amount) //how muuuch damage the player takes 
   {
     health -= amount;
     if(health <= 0 ) 
     {
       Destroy(gameObject); //if the players health is taking beloow zero then the gameobject will be destoryed 
     }
   }

}
