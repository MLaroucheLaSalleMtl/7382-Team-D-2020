using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputs1 : MonoBehaviour
{
    private Vector2 movement;

    private MyInputs playerInputActions;
    private Rigidbody2D rigid;

    [SerializeField] private float speed; //default of 4
    [SerializeField] private float upwardsVelocity; //default of 5


    private bool jump = false;
    private bool canMove = false; // Better idea to tie this thing to death menu panel display timer

    private void Awake()
    {
        playerInputActions = new MyInputs();
        rigid = GetComponent<Rigidbody2D>();
      
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        Debug.Log(movement);
        //rigid.MovePosition(rigid.position + movement * speed * Time.fixedDeltaTime);
        rigid.AddForce(transform.right * speed * movement);

        if (jump)
        {
            Debug.Log("jump");
            rigid.AddForce(transform.up * upwardsVelocity,ForceMode2D.Impulse);
            jump = false;
        }

    }

    private void OnEnable()
    {
        playerInputActions.Enable();
        Invoke("CanMove", 1f);
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            movement = context.ReadValue<Vector2>();
        }
    }

    public void OnStop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movement = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if(context.performed)
        {

            jump = true;
        }

    }

    public void HasJumped() => jump = false;
    private void CanMove() => canMove = true;
}