using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoaderManager: MonoBehaviour
{
    
    [Tooltip("Enter the Scene Name you want to load")]
    [SerializeField] private string sceneName = "";
    [SerializeField] private bool activateScene = false;
    private AsyncOperation async = null;
    private float waitForSeconds = 3f;

    public static SceneLoaderManager instance = null;
    private MusicManager mm = null;

    private void Awake()
    {
        CreateSingleton();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Screen.SetResolution(1280, 720, false);

        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = activateScene;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (FindObjectOfType<MusicManager>()) mm = GetComponent<MusicManager>();
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
        FadeOutMusic();
        yield return new WaitForSeconds(waitForSeconds);
        SetPlayerSpawnAsNull();
        SceneManager.LoadScene(name);

    }

    private IEnumerator WaitForActivateScene()
    {
        FadeOutMusic();
        yield return new WaitForSeconds(waitForSeconds);
        if (IsLoaded)
        {
            SetPlayerSpawnAsNull();
            async.allowSceneActivation = true;
        }
    }

    private void SetPlayerSpawnAsNull()
    {
        if (GameManager.instance != null) GameManager.instance.SetSpawnAsNull();
    }

    private void FadeOutMusic()
    {
        if (mm != null) mm.FadeOut();
    }

    private void CreateSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnApplicationQuit()
    {
        instance = null;
    }
}
