
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private Image loadingBar = null;
    private SceneLoaderManager sceneLoader = null;

    private void Awake()
    {
        sceneLoader = SceneLoaderManager.instance;
    }

    // Start is called before the first frame update
    private void Start()
    {
        if(GetComponent<Image>()) loadingBar = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!sceneLoader.IsLoaded)
        {
            loadingBar.fillAmount = sceneLoader.Progress;
        }
    }
}
