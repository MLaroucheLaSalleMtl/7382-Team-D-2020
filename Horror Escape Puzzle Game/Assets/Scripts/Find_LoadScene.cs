
using UnityEngine;

public class Find_LoadScene : MonoBehaviour
{

    public void LoadSceneDirectly(string name)
    {
        SceneLoaderManager.instance.LoadSceneDirectly(name);
    }
}
