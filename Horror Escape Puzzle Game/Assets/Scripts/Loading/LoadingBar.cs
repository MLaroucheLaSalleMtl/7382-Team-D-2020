
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{

    private Image loadingBar;
    [SerializeField] private SceneLoaderManager sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        loadingBar = this.GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!sceneLoader.IsLoaded)
        {
            loadingBar.fillAmount = sceneLoader.Progress;
        }

    }
}
