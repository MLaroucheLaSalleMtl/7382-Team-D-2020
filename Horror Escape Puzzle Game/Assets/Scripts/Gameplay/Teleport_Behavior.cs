
using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleport_Behavior : MonoBehaviour
{
    [SerializeField] private string nextSceneName = null;
    private SceneLoaderManager slm;

    private void Awake()
    {
        slm = SceneLoaderManager.instance;
    }

    private void Start()
    {
        if(slm != null) slm.LoadSceneAsync(nextSceneName, false);
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && slm.IsLoaded)
        {
            if (slm != null) slm.ActivateScene();
#if UNITY_EDITOR
            else throw new NullReferenceException("Scene Loader Manager is missing");
#endif
        }
    }
}
