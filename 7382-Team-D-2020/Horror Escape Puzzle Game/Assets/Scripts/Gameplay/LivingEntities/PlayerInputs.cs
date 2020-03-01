
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

    public void HasJumped() => hasJumped = false;

    private void Awake()
    {
        playerInputActions = new MyInputs();
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        this.gameObject.transform.Translate(Vector2.right * movement.x * speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        if (context.performed)
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
        if (context.started && !hasJumped)
        {
            rigid.AddForce(Vector2.up * upwardsVelocity, ForceMode2D.Impulse);
            hasJumped = true;
        }
    }


    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
}
