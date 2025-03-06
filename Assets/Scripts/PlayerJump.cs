using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerJump : MonoBehaviour
{
    public InputSystem inputs;
    public Vector3 jumpDir;
    public float jumpForce = 2.0f;
    
    public bool isGrounded;
    public bool canJump = true;
    public Rigidbody rb;

    void Start()
    {
    	jumpDir = new Vector3(0.0f, 2.0f, 0.0f);
        inputs.OnJump += Jump;
    }

    void OnCollisionStay()
    {
    	isGrounded = true;
    }
    
    void Jump(InputAction.CallbackContext context)
    {
    	if(isGrounded && canJump)
        {
            StartCoroutine(JumpCD());
    		rb.AddForce(jumpDir * jumpForce, ForceMode.Impulse);
    	}
    }

    IEnumerator JumpCD()
    {
        canJump = false;
        yield return new WaitForSeconds(0.3f);
        canJump = true;
        isGrounded = false;
    }
}
