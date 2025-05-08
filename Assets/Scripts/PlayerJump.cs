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

    public bool level2Mode = false;
    public bool level2Complete = false;

    void Start()
    {
    	jumpDir = new Vector3(0.0f, 2.0f, 0.0f);
        inputs.OnJump += Jump;
        inputs.OnDownKey += DescendPlatformManually;
        inputs.OnUpKey += AscendPlatform;
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
    
    void AscendPlatform(InputAction.CallbackContext context)
    {
        if (!level2Mode)
        {
            SetLayerMove(transform.position.z + 1);
        }
    }

    void DescendPlatformManually(InputAction.CallbackContext context)
    {
        if (!level2Mode)
        {
            isGrounded = false;
            DescendPlatform();
        }
    }

    public void DescendPlatform()
    {
        if (transform.position.z > 0.6f)
        {
            SetLayerMove(transform.position.z - 0.6f);
        }
    }

    private float zLayerMove;
    public void SetLayerMove(float zLayer)
    {
        zLayerMove = Mathf.Round(zLayer);
    }

    void FixedUpdate()
    {
        if (zLayerMove != 999999)
        {
            if (transform.position.z != zLayerMove && !level2Mode)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, zLayerMove), 5 * Time.deltaTime);
            }
            else{
                zLayerMove = 999999;
            }
        }

        if (level2Mode)
        {
            zLayerMove = transform.position.z;
            
            if (level2Complete)
            {
                transform.Translate(new Vector3(0, 0f, 1f) * 4.0f * Time.deltaTime);   
            }       
        }
    }
}
