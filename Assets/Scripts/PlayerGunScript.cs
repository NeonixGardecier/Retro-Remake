using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerGunScript : MonoBehaviour
{
    public GameObject directionalMarker;
    public InputSystem inputSystem;

    public Vector3 offset;
    public float distanceMult; 

    private InputAction.CallbackContext lastDirectional;

    [Header("Gun Settings")]
    public GameObject projectile;
    public float cooldown;
    public float lifeTime;
    public float bulletSpeed;
    private bool canFire = true;

    void Start()
    {
        inputSystem.OnMovement += UpdateDirectional;
        inputSystem.OnShootKey += Shoot;
    }

    void UpdateDirectional(InputAction.CallbackContext context)
    {
        lastDirectional = context;
        Vector2 inputVector = context.ReadValue<Vector2>();
        directionalMarker.transform.position = offset + new Vector3(transform.position.x + (inputVector.x + distanceMult), transform.position.y + (inputVector.y + distanceMult), transform.position.z + 0);
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (canFire)
        {
            Vector2 inputVector = lastDirectional.ReadValue<Vector2>();
            directionalMarker.transform.position = offset + new Vector3(transform.position.x + (inputVector.x + distanceMult), transform.position.y + (inputVector.y + distanceMult), transform.position.z + 0);
            
            GameObject spawnedProj = Instantiate(projectile, transform.position + new Vector3(0,1f,0), transform.rotation);
            spawnedProj.transform.LookAt(directionalMarker.transform);
            spawnedProj.GetComponent<BulletMove>().speed = bulletSpeed;
            Destroy(spawnedProj, lifeTime);


            StartCoroutine(ShootCooldown(cooldown));
        }
    }

    IEnumerator ShootCooldown(float cooldown)
    {
        canFire = false;
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }
}
