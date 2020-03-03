using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LoadSceneName), typeof(Collider2D))]
public class Teleport_Behavior : MonoBehaviour
{

    private LoadSceneName nextScene;


    private void Awake()
    {
        nextScene = GetComponent<LoadSceneName>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {


        Debug.Log(nextScene.IsLoaded);
        if (collision.CompareTag("Player")) Debug.Log("Player");
        if (collision.CompareTag("Player") && nextScene.IsLoaded)
        {
            Debug.Log("Activatea");
            nextScene.ActivateScene();
        }
    }
}
