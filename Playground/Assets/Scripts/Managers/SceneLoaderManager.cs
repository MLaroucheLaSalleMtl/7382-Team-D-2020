using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoaderManager: MonoBehaviour, ISceneUtility
{
    public static SceneLoaderManager Instance = null;

    private AsyncOperation async = null;
    private float waitForSeconds = 3f;

    [SerializeField] private GameObject prefabTransitionFadeOut = null;
    [SerializeField] private GameObject prefabTransitionFadeIn = null;

    private void Awake()
    {
        CreateSingleton();
    }

    /// <summary>
    /// Shows current loading progress.
    /// </summary>
    /// <returns>Returns a float that shows the percentage of loading.</returns>
    public float Progress => this.async.progress / 0.9f; //90% == finished loading 

    /// <summary>
    /// Checks if scene has finished loading.
    /// </summary>
    /// <returns>Return True and False</returns>
    public bool IsLoaded => Progress >= 1f;


    /// <summary>
    /// Activate loaded scene. [Warning] Does not check if scene has finished loading.
    /// </summary>
    public void ActivateScene()
    {
        StartCoroutine(nameof(WaitFor_ActivateScene));
    }

    /// <summary>
    /// Bypass async loading.
    /// </summary>
    public void LoadSceneDirectly(string name)
    {
        StartCoroutine(nameof(WaitFor_LoadSceneDirectly), name);
    }

    public void LoadSceneAsync(string scene)
    {
        async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;
    }

    private IEnumerator WaitFor_LoadSceneDirectly(string name)
    {
        //yield return new WaitForSeconds(waitForSeconds);
        yield return null;
        SceneManager.LoadScene(name);
        //async.allowSceneActivation = true;

        Debug.Log(nameof(SceneLoaderManager).ToUpper() + ": Scene activation = " + async.allowSceneActivation );
    }

    private IEnumerator WaitFor_ActivateScene()
    {
        yield return new WaitForSeconds(waitForSeconds);

        if (IsLoaded)
        {
            async.allowSceneActivation = true;

            Debug.Log(nameof(SceneLoaderManager).ToUpper() + ": Scene activation = " + async.allowSceneActivation);
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

    public void SceneUtil_OnActivation()
    {
        StopAllCoroutines();

        prefabTransitionFadeIn.SetActive(true);
        async.allowSceneActivation = false;
    }

    public void SceneUtil_LoadNextScene()
    {
        prefabTransitionFadeOut.SetActive(true);
        ActivateScene();
    }

    public void FadeOut()
    {
        prefabTransitionFadeOut.SetActive(true);
    }
}



