using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp = 3;

    public RoomManager level2Room;
    public bool level2Mode = false;
    public bool isTurret = false;

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
                Destroy(this.gameObject);
            }
        }
    }
}
