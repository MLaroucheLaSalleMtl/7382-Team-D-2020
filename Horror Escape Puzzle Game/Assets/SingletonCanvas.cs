
using UnityEngine;

public class SingletonCanvas : MonoBehaviour
{
    public static SingletonCanvas Instance = null;
    private void Awake()
    {
        CreateSingleton();
    }
    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        Instance = null;
    }
#endif

    private void OnApplicationQuit()
    {
        Instance = null;
    }
}
