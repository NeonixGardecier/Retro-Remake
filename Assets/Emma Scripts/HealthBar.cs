using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;


    void start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    public void SetMaxHealth(int health )
    {
        slider.maxValue = health;
        slider.value = health;

    }
    
    public void SetHealth(int health) 
    {
       slider.value = health;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("a");
        if (other.gameObject.tag == "EnemyProj")
        {
            Debug.Log("B");
            TakeDamage(20);
            other.gameObject.GetComponent<BulletMove>().DestroyThis(0f);
        }
    }
}
