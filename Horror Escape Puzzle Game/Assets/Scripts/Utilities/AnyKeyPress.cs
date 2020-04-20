
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AnyKeyPress : MonoBehaviour
{
    public UnityEvent OnEscapePressed;

    public void OnEscapePress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnEscapePressed?.Invoke();
        }
        
    }

}
