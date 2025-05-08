using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    public delegate void OnInputChanged(InputAction.CallbackContext context);

    public event OnInputChanged OnMovement;
    public event OnInputChanged OnJump;
    public event OnInputChanged OnDownKey;
    public event OnInputChanged OnUpKey;
    public event OnInputChanged OnShootKey;

    bool movingHeld;
    bool downHeld;
    public Vector2 movementInput;
    InputAction.CallbackContext movementContext;
    InputAction.CallbackContext jumpContext;

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
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
            downHeld = true;
        }
        if (context.canceled)
        {
            downHeld = false;
        }
    }

    public void OnUpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnUpKey.Invoke(context);
        }
    }

    public void OnShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnShootKey.Invoke(context);
        }
        if (context.canceled)
        {
            OnShootKey.Invoke(context);     
        }
    }
}
