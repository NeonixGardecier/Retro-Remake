using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp = 3;

    public RoomManager level2Room;
    public bool level2Mode = false;
    public bool isTurret = false;

    public GameObject powerupPrefab1;
    public GameObject powerupPrefab2;
    public GameObject powerupPrefab3;

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "PlayerProj") //If player hits this enemy
        {
            hp -= 1;
            other.gameObject.GetComponent<BulletMove>().DestroyThis(0f);

            if (hp <= 0)// if hp is zero
            {
                if (level2Mode) //On level 2 this is needed to track kills for progression
                {
                    if (isTurret)
                    {
                        level2Room.AddTurretKill();
                    }
                    else
                    {
                        level2Room.AddKill();
                    }
                }
                SpawnPowerup(); //Spawn a powerup
                Destroy(this.gameObject); //Destroy this enemy
            }
        }
    }

    void SpawnPowerup() //Chance to spawn powerups
    {
        if (Random.Range(0,100) > 50)
        {
            int rnd = Random.Range(0,100);

            if (rnd > 66){Instantiate(powerupPrefab1, transform.position, powerupPrefab1.transform.rotation);}
            else if (rnd > 33){Instantiate(powerupPrefab2, transform.position, powerupPrefab2.transform.rotation);}
            else {Instantiate(powerupPrefab3, transform.position, powerupPrefab3.transform.rotation);}
        }
    }
}
