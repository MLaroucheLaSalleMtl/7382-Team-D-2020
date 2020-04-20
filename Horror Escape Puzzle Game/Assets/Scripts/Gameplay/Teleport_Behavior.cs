
using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleport_Behavior : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(SceneLoaderManager.Instance.IsLoaded)
                SceneActivationDestroyTracker.LoadNext();
#if UNITY_EDITOR
            else
            {
                throw new NullReferenceException(nameof(Teleport_Behavior).ToUpper()
                    + ": Scene Loader Manager is missing");
            }
#endif
        }
    }
}



