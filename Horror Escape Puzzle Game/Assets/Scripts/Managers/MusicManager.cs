
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour, IGameState
{
    public static MusicManager instance = null;

    [SerializeField] private AudioClip[] levelMusicClips = null;
    [SerializeField] private AudioClip mainMenuClip = null;
    [SerializeField] private AudioClip preloaderClip = null;

    [SerializeField] private GameObject audioObj = null; //Object to instantiate
    [SerializeField] private AudioSource audioS = null; //The freaking audio source that keeps getting destroyed
    [SerializeField] private GameObject ASDF = null; // A holder for the instantiated audioObj
    private void Awake()
    {
        CreateSingleton();
        InstantiateAudiSource();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void Start()
    {
        if (audioS)
        {
            audioS.loop = true;
            audioS.volume = 0;
            StartCoroutine(IEnumFadeIn());
        }
    }

    private void SceneManager_sceneLoaded(Scene sc, LoadSceneMode loadMode)
    {
        InstantiateAudiSource();
        if (audioS)
        {
            audioS.volume = 0f;

            AudioClip clip = null;

            switch (sc.name)
            {
                case "PreloaderScene":
                    clip = preloaderClip;
                    break;

                case "MainMenuScene":
                    clip = mainMenuClip;
                    break;

                default: //Plays a Level Music

                    string sub = sc.name.Substring(sc.name.Length-1);

                    for (int i = 0; i < levelMusicClips.Length; i++)
                    {
                        if(sub == i.ToString())
                        {
                            clip = levelMusicClips[i];
                        }
                    }
                    break;
            }

        
            audioS.clip = clip;
            audioS.Play();
        }

        FadeIn();
    }

    private IEnumerator IEnumFadeIn()
    {
        float temp = 0f;
        while(audioS.volume <= 0.8)
        {
            audioS.volume = Mathf.Lerp(0f, 0.8f, temp);
            yield return null;
            temp += Time.deltaTime;
        }
    }

    private IEnumerator IEnumFadeOut()
    {
        float temp = 0f;
        while (audioS.volume != 0)
        {
            audioS.volume = Mathf.Lerp(0.8f, 0f, temp);
            yield return null;
            temp += Time.deltaTime;
        }
    }

    private void FadeIn()
    {
        if (audioS) StartCoroutine(IEnumFadeIn());
    }

    public void FadeOut()
    {
        if(audioS) StartCoroutine(IEnumFadeOut());
    }

    public void Pause() => audioS.Pause();

    public void UnPause() => audioS.UnPause();

    private void CreateSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        instance = null;
    }

    private void InstantiateAudiSource()
    {
        if (!ASDF) ASDF = Instantiate(audioObj);
        if (ASDF) audioS = ASDF.GetComponent<AudioSource>();
    }
}
