using System;

using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneActivationDestroyTracker : MonoBehaviour
{
    public static SceneActivationDestroyTracker Instance = null;

    [SerializeField] private string asyncNextSceneName = null;

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        if(asyncNextSceneName != null)
        {
            SceneLoaderManager.Instance.LoadSceneAsync(asyncNextSceneName);
        }
        else
        {
            throw new NullReferenceException
                (
                    nameof(SceneActivationDestroyTracker).ToUpper() 
                    + ": Scene Loader Manager not found!"
                );
        }

        Debug.Log("=== " + SceneManager.GetActiveScene().name.ToUpper() + " ACTIVATED===");

        GameMenuManager.Instance.SceneUtil_OnActivation();
        GameManager.Instance.SceneUtil_OnActivation();
        MusicManager.Instance.SceneUtil_OnActivation();
        SceneLoaderManager.Instance.SceneUtil_OnActivation();

        Controls.Instance.Locked = false;
        LoadSceneDirectly.Instance.HasBeenActivated = false;
    }

    public static void LoadNext()
    {
        Debug.Log("=== " + SceneManager.GetActiveScene().name.ToUpper() + " CHANGING SCENE ===");

        GameMenuManager.Instance.SceneUtil_LoadNextScene();
        GameManager.Instance.SceneUtil_LoadNextScene();
        MusicManager.Instance.SceneUtil_LoadNextScene();
        SceneLoaderManager.Instance.SceneUtil_LoadNextScene();

        Controls.Instance.Locked = true;
    } 

    private void OnDestroy()
    {
        Debug.Log("=== " + SceneManager.GetActiveScene().name.ToUpper() + " DESTROYED ===");

#if UNITY_EDITOR //Singleton Killer
        Instance = null;
#endif
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
