
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerCC : MonoBehaviour
{
    private Vector2 movement;

    private MyInputs playerInputActions;
    private CharacterController cc;

    [SerializeField] private float speed; //default of 4
    [SerializeField] private float upwardsVelocity; //default of 5

    private bool hasJumped;
    private bool canMove; // Better idea to tie this thing to death menu panel display timer

    private void Awake()
    {
        playerInputActions = new MyInputs();
        movement = new Vector2();
        cc = GetComponent<CharacterController>();
    }

    private void Start()
    {
        canMove = false;
    }

    private void FixedUpdate()
    {
        Debug.Log(IsGrounded());
        if (IsGrounded())
        {
            cc.Move(movement);
        }
        else
        {
            movement.y = -1f *Time.deltaTime;
            cc.Move(movement);
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


    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("that");
        if (context.performed && canMove)
        {
            playerInputActions.Player.Move.performed += callbackContext => movement = callbackContext.ReadValue<Vector2>();
        }
        else
        {
            movement = Vector2.zero;
        }
    }
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.down * 0.6f);
        if(hit.collider.CompareTag("Ground"))
        {       
            return true;
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down * 0.6f), Color.yellow);
        return false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && !hasJumped && canMove)
        {
            Debug.Log("this");
           movement = new Vector3(movement.x, 5);
           
            
            hasJumped = true;
        }
    }

    public void HasJumped() => hasJumped = false;
    private void CanMove() => canMove = true;
}
