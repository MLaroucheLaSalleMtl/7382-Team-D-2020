
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{

    public static Controls Instance = null;

    public bool Locked = false;

    [SerializeField] private float inputLockTimer = 0f; // TODO: Link to the something instead of gettign vlaue

    public UnityAction UAction_OnCancel;
    public UnityAction UAction_OnControllerClick;
    public UnityAction UAction_OnEscapePress;
    public UnityAction UAction_OnJump;
    public UnityAction UAction_OnClick;
    public UnityAction<Vector2> UAction_OnMove;
    public UnityAction<bool,Vector2> UAction_OnCursorNavigate;

    private void Awake()
    {
        CreateSingleton();
    }

    private void OnEnable()
    {
        Invoke(nameof(Unlock), inputLockTimer);
    }

    private void Unlock() => Locked = false;


#if UNITY_EDITOR
    private void Update() //For Testing only
    {
        //print(Mouse.current.position.ReadValue());
    }
#endif

    public void OnMove(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        Debug.Log(nameof(Controls) + ": Move" + context.ReadValue<Vector2>().x);
#endif
        if (!Locked)
        {
            UAction_OnMove?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        Debug.Log(nameof(Controls) + ": Jump Press");
#endif
        if (!Locked && context.started)
        {
            UAction_OnJump?.Invoke();
        }
    }
    public void OnEscapePress(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        Debug.Log(nameof(Controls) + ": Escape Toggle");
#endif
        if (!Locked && context.started)
        {
            UAction_OnEscapePress?.Invoke();
        }
    }

    public void OnPointerMove(InputAction.CallbackContext context)
    {

#if UNITY_EDITOR
        Debug.Log(nameof(Controls) + ": Controller Cursor Navigate " + context.ReadValue<Vector2>()
            + "\nCurrent Position: " + Mouse.current.position.ReadValue());
#endif
        if (!Locked )
        { 
            UAction_OnCursorNavigate?.Invoke(context.performed,context.ReadValue<Vector2>());
        }
    }

    public void OnControllerClick(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        Debug.Log(nameof(Controls) + ": Cursor Click" + Mouse.current.leftButton.isPressed);
#endif

        if (!Locked )
        {
            UAction_OnControllerClick?.Invoke();

        }
    }

    public void OnControllerCancel(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        Debug.Log(nameof(Controls) + ": Button Cancel");
#endif
        if(!Locked && context.performed)
        {
            UAction_OnCancel?.Invoke();
        }
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
#if UNITY_EDITOR
            DestroyImmediate(gameObject);
#else
            Destroy(gameObject);
#endif
        }
    }

    private void OnApplicationQuit()
    {
        Instance = null;
    }
}
