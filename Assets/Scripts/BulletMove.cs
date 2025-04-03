using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;

    public bool isHoming = false;
    public Transform homingTarget;
    
    void Update()
    {
        if (!isHoming)
        {
            Vector3 offset = new Vector3(0,-0.2f,0);
            Vector3 spherePos = transform.position + (Vector3.forward * 3) + offset;

            Collider[] hitColliders = Physics.OverlapSphere(spherePos, 3);

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


}
