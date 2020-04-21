
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    
    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerExitEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnterEvent?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExitEvent?.Invoke();
    }
}
