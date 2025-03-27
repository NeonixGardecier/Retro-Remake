using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerGunScript : MonoBehaviour
{
    public InputSystem inputSystem;
    public Vector3 offset;
    public float distanceMult; 
    private Vector2 lastDirectional;

    [Header("Markers")]
    public GameObject directionalMarker;
    public GameObject shotgunMarker1;
    public GameObject shotgunMarker2;

    [Header("Gun Settings")]
    public GameObject projectile;
    public float cooldown;
    public float lifeTime;
    public float bulletSpeed;
    private bool canFire = true;
    public enum FiringModes {none, shotgun, rapid, MachineGun};
    public FiringModes selectedFiringMode;

    void Start()
    {
        inputSystem.OnMovement += UpdateDirectional;
        inputSystem.OnShootKey += Shoot;

        lastDirectional = new Vector2(1,0);
    }

    void UpdateDirectional(InputAction.CallbackContext context)
    {        
        Vector2 inputVector = context.ReadValue<Vector2>();
        lastDirectional = inputVector;
        UpdateMarker(inputVector);
    }

    void UpdateMarker(Vector2 inputVector)
    {
        if (inputVector.y == 0 && inputVector.x == 0)
        {
            inputVector.x = lastDirectional.x;
        }
        directionalMarker.transform.position = offset + new Vector3(transform.position.x + (inputVector.x + distanceMult), transform.position.y + (inputVector.y + distanceMult), transform.position.z + 0);
    }

    private bool isShooting;
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isShooting = true;
        }
        if (context.canceled)
        {
            isShooting = false;
        }
    }

    void Update()
    {
        if (isShooting)
        {
            switch(selectedFiringMode)
            {
                case FiringModes.none:
                    FireMode_Standard();
                break;

                case FiringModes.shotgun:
                    FireMode_Shotgun();
                break;

                case FiringModes.rapid:
                    FireMode_Rapid();
                break;
    
                case FiringModes.MachineGun:
                    FireMode_MachineGun();
                break;
            }
        }
    }

    IEnumerator ShootCooldown(float cooldown)
    {
        canFire = false;
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }
    
    void FireMode_Standard()
    {
        if (canFire)
        {
            Vector2 inputVector = lastDirectional;
            UpdateMarker(inputVector);
            
            GameObject spawnedProj = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj.transform.LookAt(directionalMarker.transform);
            spawnedProj.GetComponent<BulletMove>().speed = bulletSpeed;
            Destroy(spawnedProj, lifeTime);

            StartCoroutine(ShootCooldown(cooldown));
        }
    }

    void FireMode_Rapid()
    {
        if (canFire)
        {
            Vector2 inputVector = lastDirectional;
            UpdateMarker(inputVector);
            
            GameObject spawnedProj = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj.transform.LookAt(directionalMarker.transform);
            spawnedProj.GetComponent<BulletMove>().speed = bulletSpeed;
            Destroy(spawnedProj, lifeTime);

            StartCoroutine(ShootCooldown(cooldown / 3));
        }       
    }

    void FireMode_Shotgun()
    {
        if (canFire)
        {
            Vector2 inputVector = lastDirectional;
            UpdateMarker(inputVector);

            GameObject spawnedProj = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj.transform.LookAt(directionalMarker.transform);
            spawnedProj.GetComponent<BulletMove>().speed = bulletSpeed;
            Destroy(spawnedProj, lifeTime / 2);

            GameObject spawnedProj1 = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj1.transform.LookAt(shotgunMarker1.transform);
            spawnedProj1.GetComponent<BulletMove>().speed = bulletSpeed;
            Destroy(spawnedProj1, lifeTime / 2);

            GameObject spawnedProj2 = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj2.transform.LookAt(shotgunMarker2.transform);
            spawnedProj2.GetComponent<BulletMove>().speed = bulletSpeed;
            Destroy(spawnedProj2, lifeTime / 2);

            StartCoroutine(ShootCooldown(cooldown * 1.5f));
        }
    }

    void FireMode_MachineGun()
    {
        if (canFire)
        {
            Vector2 inputVector = lastDirectional;
            UpdateMarker(inputVector);
            
            GameObject spawnedProj = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj.transform.LookAt(directionalMarker.transform.position + new Vector3(Random.Range(-0.18f, 0.18f), Random.Range(-0.18f, 0.18f), 0));
            spawnedProj.GetComponent<BulletMove>().speed = bulletSpeed * 1.25f;
            Destroy(spawnedProj, lifeTime);

            StartCoroutine(ShootCooldown(cooldown / 4.5f));
        }       
    }
}
