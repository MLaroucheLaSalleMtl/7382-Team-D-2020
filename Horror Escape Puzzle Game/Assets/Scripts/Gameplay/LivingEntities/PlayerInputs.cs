
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{

    private MyInputs playerInputActions;
    private bool locked = false;

    [SerializeField] private float inputLockTimer = 0f; // TODO: Link to the something instead of gettign vlaue

    public UnityAction OnJumpCtx;
    public UnityAction OnPauseToggle; 

    private void Awake()
    {
        playerInputActions = new MyInputs();
    }
    private void Start()
    {
        AddListeners();
    }
    private void OnEnable()
    {
        Invoke("Unlock", inputLockTimer);
        playerInputActions.Enable();
    }

    private void Unlock() => locked = false; // Invoke Call

    public void OnMoveCall(InputAction.CallbackContext context)
    {
        if(!locked) SetMovementVals(context.ReadValue<Vector2>());
    }
    private void SetMovementVals(Vector2 direction)
    {
        gameObject.GetComponent<Player_Behavior>().SetMovement(direction);
    }

    public void OnJumpCall(InputAction.CallbackContext context)
    {
        if (!locked && context.started) OnJumpCtx?.Invoke();
    }
    public void OnEscapeToggle(InputAction.CallbackContext context)
    {
        Debug.Log("Excape toggle");
        if(context.started) OnPauseToggle?.Invoke();
    }

    private void AddListeners()
    {
        OnJumpCtx += GetComponent<Player_Behavior>().Jump;
        OnPauseToggle += GameManager.instance.ChangeGameState;
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }
}