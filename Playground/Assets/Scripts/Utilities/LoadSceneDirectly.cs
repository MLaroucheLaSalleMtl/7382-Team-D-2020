using System.Collections;

using UnityEngine;

public class LoadSceneDirectly : MonoBehaviour
{
   
    [HideInInspector]public bool HasBeenActivated = false;

    public void LoadScene(string name)
    {
        if (!HasBeenActivated) 
        {
            SceneLoaderManager.Instance.LoadSceneDirectly(name);
            HasBeenActivated = true;
        }
    }

    public void LoadSceneWait(string name)
    {
        if (!HasBeenActivated)
        {
            StartCoroutine(WaitForSeconds(name));
            
            HasBeenActivated = true;
        }
    }

    private IEnumerator WaitForSeconds(string name)
    {
        yield return new WaitForSeconds(3.5f);
        SceneLoaderManager.Instance.LoadSceneDirectly(name);
    }

}
