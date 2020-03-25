
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputs1 : MonoBehaviour
{

    private MyInputs playerInputActions;
    private bool locked = true;

    [SerializeField] private float inputLockTimer = 0f; // TODO: Link to the something instead of gettign vlaue

    public UnityEvent OnJumpCtx;

    private void Awake()
    {
        playerInputActions = new MyInputs();

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
        gameObject.GetComponent<Player_Behavior1>().SetMovement(direction);
    }

    public void OnJumpCall(InputAction.CallbackContext context)
    {
        if (!locked) OnJumpCtx?.Invoke();
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