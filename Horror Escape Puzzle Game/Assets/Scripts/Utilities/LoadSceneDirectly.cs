
using UnityEngine;

public class LoadSceneDirectly : MonoBehaviour
{
    public static LoadSceneDirectly Instance = null;

    [HideInInspector]public bool HasBeenActivated = false;

    private void Awake()
    {
        CreateSingleton();
    }

    public void LoadScene(string name)
    {
        if (!HasBeenActivated) 
        {
            SceneLoaderManager.Instance.LoadSceneDirectly(name);
            HasBeenActivated = true;
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
