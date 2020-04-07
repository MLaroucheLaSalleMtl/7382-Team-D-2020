using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoaderManager: MonoBehaviour
{

    [Tooltip("Enter the Scene Name you want to load")]
    [SerializeField] private string sceneName;
    [SerializeField] private bool activateScene;
    private AsyncOperation async;
    private float waitForSeconds = 1.2f;

    public static SceneLoaderManager instance = null; 

    private void Awake()
    {
        CreateSingleton();

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    private void Start()
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = activateScene;
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
    public bool IsLoaded => Progress == 1f;


    /// <summary>
    /// Activate loaded scene. [Warning] Does not check if scene has finished loading.
    /// </summary>
    public void ActivateScene()
    {
        StartCoroutine("WaitForActivateScene");
    }

    /// <summary>
    /// Bypass async loading.
    /// </summary>
    public void LoadSceneDirectly(string name)
    {
        StartCoroutine("WaitForLoadSceneDirectly", name);
    }

    public void LoadSceneAsync(string scene, bool allowActivation)
    {
        async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = allowActivation;
    }

    private IEnumerator WaitForLoadSceneDirectly(string name)
    {
        yield return new WaitForSeconds(waitForSeconds);
        SceneManager.LoadScene(name);
        GameManager.instance.currentSpawnPoint = null;
    }
    private IEnumerator WaitForActivateScene()
    {
        yield return new WaitForSeconds(waitForSeconds);
        if (IsLoaded)
        {
            async.allowSceneActivation = true;
            GameManager.instance.currentSpawnPoint = null;
        }
    }

    private void CreateSingleton()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
