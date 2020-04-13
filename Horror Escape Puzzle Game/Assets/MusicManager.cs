using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;

    [SerializeField] private AudioClip[] levelMusicClips;
    [SerializeField] private AudioClip mainMenuClip;
    [SerializeField] private AudioClip preloaderClip;

    private AudioSource audioS = null;

    private void Awake()
    {
        CreateSingleton();

        audioS = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        DontDestroyOnLoad(gameObject);
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
        audioS.clip = clip;
        audioS.Play();

        StartCoroutine(IEnumFadeIn());
    }

    private IEnumerator IEnumFadeIn()
    {
        float temp = 0f;
        while(audioS.volume <= 0.8)
        {
            audioS.volume = Mathf.Lerp(0f, 0.8f, temp);
            Debug.Log(audioS.volume);
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
            Debug.Log(audioS.volume);
            yield return null;
            temp += Time.deltaTime;
        }
    }

    public void FadeOut()
    {
        StartCoroutine(IEnumFadeOut());
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
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        instance = null;
    }
}
