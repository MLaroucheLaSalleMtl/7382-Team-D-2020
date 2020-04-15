using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour, IGameState
{
    public static MusicManager instance = null;

    [SerializeField] private AudioClip[] levelMusicClips;
    [SerializeField] private AudioClip mainMenuClip;
    [SerializeField] private AudioClip preloaderClip;

    private AudioSource audioS = null;

    private void Awake()
    {

        CreateSingleton();
        
        if(GetComponent<AudioSource>()) audioS = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void Start()
    {
        audioS.loop = true;
        audioS.volume = 0;
        StartCoroutine(IEnumFadeIn());
    }

    private void SceneManager_sceneLoaded(Scene sc, LoadSceneMode loadMode)
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

        if (audioS)
        {
            audioS.clip = clip;
            audioS.Play();
        }

        FadeIn();
    }

    private void FadeIn()
    {
        if(audioS) StartCoroutine(IEnumFadeIn());
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

    public void FadeOut()
    {
        if(audioS) StartCoroutine(IEnumFadeOut());
    }
    public void Pause() => audioS.Pause();
    public void UnPause() => audioS.UnPause();
    private void CreateSingleton()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnApplicationQuit()
    {
        instance = null;
        Destroy(instance);
    }
}
