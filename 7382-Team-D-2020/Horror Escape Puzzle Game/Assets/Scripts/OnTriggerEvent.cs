
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    
    public UnityEvent OnTriggerEnterEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnterEvent?.Invoke();
    }
}
