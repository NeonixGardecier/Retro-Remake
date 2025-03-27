using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 10f;
    Rigidbody2D rb;  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Make sure the component name is correctly capitalized (Rigidbody2D)
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);  // Use 'velocity' instead of 'linearVelocity'
    }
}
