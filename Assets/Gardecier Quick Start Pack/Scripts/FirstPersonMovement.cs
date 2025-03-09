using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
{
    public InputSystem inputs;
    public float speed;

    void Start()
    {
        inputs.OnMovement += Movement;
    }

    public void Movement(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        transform.Translate(new Vector3(inputVector.x, 0f, 0f) * speed * Time.deltaTime);
    }
}
