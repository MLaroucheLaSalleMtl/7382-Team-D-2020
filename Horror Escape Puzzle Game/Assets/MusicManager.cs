
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clipList;

    private AudioSource audioS = null;

    private string[] sceneNames;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();


        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;

        GetAllSceneNames();

        DontDestroyOnLoad(gameObject);
    }

    private void GetAllSceneNames()
    {
        int count = SceneManager.sceneCountInBuildSettings;

        sceneNames = new string[count];

        for (int i = 0; i < count; i++)
        {
            sceneNames[i] = Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path);
        }
    }

    private void SceneManager_sceneUnloaded(Scene sc)
    {
        // kill the music
    }

    private void SceneManager_sceneLoaded(Scene sc, LoadSceneMode loadMode)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (sc.name == sceneNames[i])
            {
                // play the corresponding music
                Debug.Log("Found Scene");
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


}
