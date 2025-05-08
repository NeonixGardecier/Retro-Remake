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
        if (other.gameObject.tag == "PlayerProj")
        {
            hp -= 1;
            other.gameObject.GetComponent<BulletMove>().DestroyThis(0f);

            if (hp <= 0)
            {
                if (level2Mode)
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
                SpawnPowerup();
                Destroy(this.gameObject);
            }
        }
    }

    void SpawnPowerup()
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
