

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneDirectly : MonoBehaviour
{
    public void LoadScene(string name)
    {
        Debug.Log("Standalone Load SCene");
        SceneManager.LoadScene(name);
    }
}
