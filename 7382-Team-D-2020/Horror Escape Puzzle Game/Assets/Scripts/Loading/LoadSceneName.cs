
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneName : MonoBehaviour
{
    [Header("Details")]
    [Tooltip("Enter the Scene Name you want to load")]
    [SerializeField] private string sceneName;

    private AsyncOperation async;


    // Start is called before the first frame update
    private void Start()
    {

        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

       
    }

    /// <summary>
    /// Shows current loading progress.
    /// </summary>
    /// <returns>Returns a float that shows the percentage of loading.</returns>
    public float Progress()
    {
        return this.async.progress / 0.9f; //90% == finished loading 
    }

    /// <summary>
    /// Checks if scene has finished loading.
    /// </summary>
    /// <returns>Return True and False</returns>
    public bool IsLoaded()
    {
        return Progress() == 1f;
    }

    /// <summary>
    /// Activate loaded scene. [Warning] Does not check if scene has finished loading.
    /// </summary>
    public void ActivateScene()
    {
        Debug.Log(IsLoaded());
        if(IsLoaded())async.allowSceneActivation = true;
    }
}
