using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;

    public bool isHoming = false;
    public Transform homingTarget;

    public GameObject ExplosionEffect;
    
    void Update()
    {
        if (!isHoming && this.gameObject.tag == "PlayerProj") //Checks if projectile should be given aim assist
        {
            Vector3 offset = new Vector3(0,-0.3f,0);
            Vector3 spherePos = transform.position + (transform.forward * 2.5f) + offset;

            Collider[] hitColliders = Physics.OverlapSphere(spherePos, 1.5f); //Use an overlap to hit the first enemy tagged object

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "Enemy") //Set homing target
                {
                    homingTarget = hitCollider.gameObject.transform;
                    isHoming = true;
                }
            }
        }

        if (isHoming) //Move proj at homing target
        {
            transform.LookAt(homingTarget);
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);        
    }

    public void EnableGravity()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void DestroyThis(float time)
    {
        Destroy(Instantiate(ExplosionEffect, transform.position, transform.rotation), 0.25f);
        Destroy(this.gameObject, time);
    }

}
