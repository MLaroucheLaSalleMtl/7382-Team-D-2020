
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AnyKeyPress : MonoBehaviour
{
    public UnityEvent OnAnyKey;

    public void OnEscapePress()
    {
        OnAnyKey?.Invoke();
        //inputAction.OnEscapeToggle(InputAction.CallbackContext context);
    }

}
