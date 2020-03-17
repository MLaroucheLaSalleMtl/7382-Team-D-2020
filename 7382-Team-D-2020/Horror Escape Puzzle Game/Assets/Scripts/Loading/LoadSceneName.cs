
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneName : MonoBehaviour
{

    [Tooltip("Enter the Scene Name you want to load")]
    [SerializeField] private string sceneName;
    [SerializeField] private bool activateScene;
    private AsyncOperation async;


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
        if(IsLoaded)async.allowSceneActivation = true;
    }

    /// <summary>
    /// Bypass async loading.
    /// </summary>
    public void LoadSceneDirectly(string name)
    {
        SceneManager.LoadScene(name);
    }
}
