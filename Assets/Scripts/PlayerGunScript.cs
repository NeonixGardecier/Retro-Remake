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
    public bool level2Mode = false;

    void Start()
    {
        inputSystem.OnShootKey += Shoot;

        lastDirectional = new Vector2(1,0); //Last directional is used when you shoot without a move input
    }

    void FixedUpdate()
    {        
        Vector2 inputVector = inputSystem.movementInput;
        lastDirectional = inputVector;
        UpdateMarker(inputVector);
    }

    void UpdateMarker(Vector2 inputVector) //Update position of shooting marker which is where the gun shoot towards
    {
        if (inputVector.y == 0 && inputVector.x == 0) //If no input use last input
        {
            inputVector.x = lastDirectional.x;
        }
        directionalMarker.transform.position = offset + new Vector3(transform.position.x + (inputVector.x + distanceMult), transform.position.y + (inputVector.y + distanceMult), transform.position.z + 0);

        if (level2Mode) //On level two, set to forward
        {
            directionalMarker.transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z + 1.5f);
        }
    }

    private bool isShooting;
    public void Shoot(InputAction.CallbackContext context) //Hold shoot bool
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
        if (isShooting) //Decide what to shoot
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
    
    void FireMode_Standard() //Shoot single
    {
        if (canFire)
        {
            Vector2 inputVector = lastDirectional;
            UpdateMarker(inputVector);
            
            GameObject spawnedProj = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj.transform.LookAt(directionalMarker.transform);
            spawnedProj.GetComponent<BulletMove>().speed = bulletSpeed;
            spawnedProj.GetComponent<BulletMove>().DestroyThis(lifeTime);

            StartCoroutine(ShootCooldown(cooldown));
        }
    }

    void FireMode_Rapid() //Shoot fast
    {
        if (canFire)
        {
            Vector2 inputVector = lastDirectional;
            UpdateMarker(inputVector);
            
            GameObject spawnedProj = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj.transform.LookAt(directionalMarker.transform);
            spawnedProj.GetComponent<BulletMove>().speed = bulletSpeed;
            spawnedProj.GetComponent<BulletMove>().DestroyThis(lifeTime);

            StartCoroutine(ShootCooldown(cooldown / 3));
        }       
    }

    void FireMode_Shotgun() //Shoot shotgun
    {
        if (canFire)
        {
            Vector2 inputVector = lastDirectional;
            UpdateMarker(inputVector);

            GameObject spawnedProj = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj.transform.LookAt(directionalMarker.transform);
            spawnedProj.GetComponent<BulletMove>().speed = bulletSpeed;
            spawnedProj.GetComponent<BulletMove>().DestroyThis(lifeTime);

            GameObject spawnedProj1 = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj1.transform.LookAt(shotgunMarker1.transform);
            spawnedProj1.GetComponent<BulletMove>().speed = bulletSpeed;
            spawnedProj.GetComponent<BulletMove>().DestroyThis(lifeTime);

            GameObject spawnedProj2 = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj2.transform.LookAt(shotgunMarker2.transform);
            spawnedProj2.GetComponent<BulletMove>().speed = bulletSpeed;
            spawnedProj.GetComponent<BulletMove>().DestroyThis(lifeTime);

            StartCoroutine(ShootCooldown(cooldown * 1.5f));
        }
    }

    void FireMode_MachineGun() //SHoot even faster
    {
        if (canFire)
        {
            Vector2 inputVector = lastDirectional;
            UpdateMarker(inputVector);
            
            GameObject spawnedProj = Instantiate(projectile, transform.position + new Vector3(0,1f + offset.y, 0), transform.rotation);
            spawnedProj.transform.LookAt(directionalMarker.transform.position + new Vector3(Random.Range(-0.18f, 0.18f), Random.Range(-0.18f, 0.18f), 0));
            spawnedProj.GetComponent<BulletMove>().speed = bulletSpeed * 1.25f;
            spawnedProj.GetComponent<BulletMove>().DestroyThis(lifeTime);

            StartCoroutine(ShootCooldown(cooldown / 4.5f));
        }       
    }
    
    void OnCollisionEnter(Collision other) //Detect walking into powerup
    {
        if (other.gameObject.tag == ("shotGun"))
        {
            StartCoroutine(ActivatePowerUp(FiringModes.shotgun));
            Destroy(other.gameObject);
        }
      
        else if (other.gameObject.tag == ("MachineGun"))
        {
            StartCoroutine(ActivatePowerUp(FiringModes.MachineGun));
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag ==("rapidFire")){
            StartCoroutine(ActivatePowerUp(FiringModes.rapid));
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag ==("fireBall")){
            StartCoroutine(ActivatePowerUp(FiringModes.shotgun));
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == ("spreadGun")){Destroy(other.gameObject);}
    }


IEnumerator ActivatePowerUp(FiringModes fireMode) //Powerup coroutine
{
    selectedFiringMode = fireMode;
    yield return new WaitForSeconds(4f);
    selectedFiringMode = FiringModes.none;
} 

}
