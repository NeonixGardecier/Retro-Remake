using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp = 3;

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "PlayerProj")
        {
            hp -= 1;
            Destroy(other.gameObject);

            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
