
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputs : MonoBehaviour
{
    private Vector2 movement;

    private MyInputs playerInputActions;
    private Rigidbody2D rigid;

    [SerializeField] private float speed; //default of 4
    [SerializeField] private float upwardsVelocity; //default of 5

    private bool hasJumped;
    private bool canMove; // Better idea to tie this thing to death menu panel display timer

    private void Awake()
    {
        playerInputActions = new MyInputs();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        canMove = false;
    }

    private void FixedUpdate()
    {
        this.gameObject.transform.Translate(Vector2.right * movement.x * speed * Time.deltaTime);
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

        if (context.performed && canMove)
        {
            playerInputActions.Player.Move.performed += callbackContext => movement = callbackContext.ReadValue<Vector2>();
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && !hasJumped && canMove)
        {
            rigid.AddForce(Vector2.up * upwardsVelocity, ForceMode2D.Impulse);
            hasJumped = true;
        }
    }

    public void HasJumped() => hasJumped = false;
    private void CanMove() => canMove = true;
}
