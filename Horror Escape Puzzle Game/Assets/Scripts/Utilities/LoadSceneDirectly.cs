
using UnityEngine;

public class LoadSceneDirectly : MonoBehaviour
{
    private bool hasBeenActivated = false;
    public void LoadScene(string name)
    {
        if (!hasBeenActivated) 
        {
            SceneLoaderManager.Instance.LoadSceneDirectly(name);
            hasBeenActivated = true;
        }
    }
}
