
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleport_Behavior : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    private SceneLoaderManager slm;

    private void Awake()
    {
        slm = SceneLoaderManager.instance;
    }

    private void Start()
    {
        slm?.LoadSceneAsync(nextSceneName, false);
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && slm.IsLoaded)
        {
            slm?.ActivateScene();
        }
    }
}
