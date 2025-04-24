using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;

    public bool isHoming = false;
    public Transform homingTarget;

    public GameObject ExplosionEffect;
    
    void Update()
    {
        if (!isHoming && this.gameObject.tag == "PlayerProj")
        {
            Vector3 offset = new Vector3(0,-0.3f,0);
            Vector3 spherePos = transform.position + (transform.forward * 2.5f) + offset;

            Collider[] hitColliders = Physics.OverlapSphere(spherePos, 1.5f);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "Enemy")
                {
                    homingTarget = hitCollider.gameObject.transform;
                    isHoming = true;
                }
            }
        }

        if (isHoming)
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
