using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretScript : MonoBehaviour
{
    public Transform player;
    public GameObject projectile;
    public Transform barrel;

    [Header("Settings")]
    public bool hasGravity;
    public float aggroRange;
    public float attackSpeed;
    public float projSpeed;

    private bool canFire = true;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            Vector3 dropOffAdujustment = new Vector3(0,0.4f,0);      
            if (hasGravity){dropOffAdujustment = new Vector3(0,2.5f,0);}

            barrel.LookAt(player.position + dropOffAdujustment);

            if (canFire)
            {
                GameObject spawnedProj = Instantiate(projectile, barrel.position + (barrel.forward * 1), barrel.rotation);
                BulletMove projScript = spawnedProj.GetComponent<BulletMove>();

                projScript.speed = projSpeed;

                if (hasGravity){projScript.EnableGravity();}

                StartCoroutine(FireCooldown());
            }
        }
    }

    IEnumerator FireCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(1 / attackSpeed);
        canFire = true;
    }
}
