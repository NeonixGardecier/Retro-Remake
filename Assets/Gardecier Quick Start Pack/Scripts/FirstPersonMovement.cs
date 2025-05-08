using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public InputSystem inputs;
    public float speed;
    
    Animator anim;
    InputSystem inputSys;

    void Awake()
    {
        inputSys = FindObjectOfType<InputSystem>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update() 
    {
        Movement();
    }

    public void Movement()
    {
        anim.SetFloat("MovementBlend", Mathf.Abs(inputSys.movementInput.x));
        //Debug.Log(Mathf.Abs(inputSys.movementInput.x));
        transform.Translate(new Vector3(inputSys.movementInput.x, 0f, 0f) * speed * Time.deltaTime);
    }
}
