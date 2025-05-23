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

    public GameObject jumpDetector;

    void Start()
    {
    	jumpDir = new Vector3(0.0f, 2.0f, 0.0f);
        inputs.OnJump += Jump;
        inputs.OnDownKey += DescendPlatformManually;
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

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "MoveBackInSceneTrigger")
        {
            SetLayerMove(transform.position.z + 1);
        }

        if (other.gameObject.tag == "MoveDownInSceneTrigger")
        {
            transform.position += new Vector3(0,-1.2f,0);
        }

        if (other.gameObject.tag == "MoveForwardInSceneTrigger")
        {
            DescendPlatform();
        }
    }

    void DescendPlatformManually(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            isGrounded = false;
            DescendPlatform();
        }
    }

    public void DescendPlatform()
    {
        if (transform.position.z > 0.6f)
        {
            SetLayerMove(transform.position.z - 1);;
        }
    }

    private float zLayerMove;
    public void SetLayerMove(float zLayer)
    {
        zLayerMove = Mathf.Round(zLayer);
    }

    void Update()
    {
        if (zLayerMove != 999999)
        {
            if (transform.position.z != zLayerMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, zLayerMove), 5 * Time.deltaTime);
            }
            else{
                zLayerMove = 999999;
            }
        }
    }
}
