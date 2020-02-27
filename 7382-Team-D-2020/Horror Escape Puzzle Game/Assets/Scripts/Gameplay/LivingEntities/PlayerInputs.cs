
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputs : MonoBehaviour
{
    //private CmovementaracterController2D cmovementaracter;

    private Vector2 movement;

    private MyInputs playerInputActions;
    private Rigidbody2D rigid;

    [SerializeField] private float speed;
    [SerializeField] private float upwardsVelocity;

    private void Awake()
    {
        playerInputActions = new MyInputs();
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("OnMove" + movement);
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
        Debug.Log("Jumping");
        if (context.started)
        {
            rigid.AddForce(Vector2.up * upwardsVelocity, ForceMode2D.Impulse);
        }
    }

    // Start is called before tmovemente first frame update
    void Start()
    {

       rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         this.gameObject.transform.Translate(Vector2.right * movement.x * speed * Time.deltaTime);
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
