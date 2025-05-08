using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// 
///     THIS SCRIPT IS UNUSED
/// 
/// 
/// </summary>
public class PlayerPlatformScript : MonoBehaviour
{
    private InputSystem inputSystem;

    public Collider playerCollider;
    private Collider thisPlatformCollider;

    void Start()
    {
        thisPlatformCollider = GetComponent<Collider>();
        inputSystem = GameObject.Find("INPUT MANAGER").GetComponent<InputSystem>();

        inputSystem.OnDownKey += DescendPlatform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlatformDetector")
        {
            playerCollider = collision.gameObject.GetComponent<PlatformDetector>().playerCollider;

            //Check if the player is above the platform
            if (playerCollider.transform.position.y > transform.position.y)
            {
                return;
            }

            //if player isnt above disable collision
            Physics.IgnoreCollision(playerCollider, thisPlatformCollider);
        }

        if (collision.gameObject.tag == "Player")
        {
            playerCollider = collision.collider;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCollider = null;
        }
    }

    void Update()
    {
        if (playerCollider != null)
        {
            if (playerCollider.transform.position.y > transform.position.y)
            {
                Physics.IgnoreCollision(playerCollider, thisPlatformCollider, false);
            }
        }
    }

    void DescendPlatform(InputAction.CallbackContext context)
    {
        if (playerCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, thisPlatformCollider);
            StartCoroutine(EnableCollisionAfterDelay(playerCollider, 1.5f));
            playerCollider = null;
        }
    }

    IEnumerator EnableCollisionAfterDelay(Collider playerCol, float delay)
    {
        yield return new WaitForSeconds(delay);
        Physics.IgnoreCollision(playerCol, thisPlatformCollider, false);   
        playerCollider = null;
    }
}
