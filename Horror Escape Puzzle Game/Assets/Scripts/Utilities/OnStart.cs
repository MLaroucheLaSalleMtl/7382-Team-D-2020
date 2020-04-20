
using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour
{

    public UnityEvent OnStarting = new UnityEvent();
    private void Start()
    {
        OnStarting?.Invoke();
    }
}
