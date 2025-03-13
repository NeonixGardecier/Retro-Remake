using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    public delegate void OnInputChanged(InputAction.CallbackContext context);

    public event OnInputChanged OnMovement;
    public event OnInputChanged OnJump;
    public event OnInputChanged OnDownKey;
    public event OnInputChanged OnShootKey;

    bool movingHeld;
    InputAction.CallbackContext movementContext;
    InputAction.CallbackContext jumpContext;


    void Update()
    {
        if (movingHeld)
        {
            OnMovement.Invoke(movementContext);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        { 
            movingHeld = true;
            movementContext = context;
        }
        if(context.canceled)
        {
            movingHeld = false;
        }

    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        { 
            jumpContext = context;
            OnJump.Invoke(jumpContext);         
        }
    }

    public void OnDownInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnDownKey.Invoke(context);
        }
    }

    public void OnShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnShootKey.Invoke(context);
        }
    }
}
