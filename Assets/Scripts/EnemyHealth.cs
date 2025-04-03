using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp = 3;

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "PlayerProj")
        {
            hp -= 1;
            other.gameObject.GetComponent<BulletMove>().DestroyThis(0f);

            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
