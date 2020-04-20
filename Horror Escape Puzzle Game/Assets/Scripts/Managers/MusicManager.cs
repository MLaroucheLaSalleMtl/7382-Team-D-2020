
using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour, IGameState, ISceneUtility
{
    public static MusicManager Instance = null;

    [SerializeField] private AudioClip[] levelMusicClips = null;
    [SerializeField] private AudioClip mainMenuClip = null;
    [SerializeField] private AudioClip preloaderClip = null;

    [SerializeField] private AudioSource audioS = null; 

    private void Awake()
    {
        CreateSingleton();

        Debug.Log(nameof(MusicManager) + " Awake");
    }

    private IEnumerator IEnumFadeIn()
    {
        float temp = 0f;

        while(audioS.volume <= 0.8f)
        {
            audioS.volume = Mathf.Lerp(0f, 0.8f, temp);
            yield return null;
            temp += Time.deltaTime;

            //Debug.Log(nameof(MusicManager) + ": Temp: " + temp + " | Volume: " + audioS.volume);
            if (audioS.volume >= 0.8f)
            {
                StopCoroutine(IEnumFadeIn());
                yield return null;
            }
        }
    }

    private IEnumerator IEnumFadeOut()
    {
        float temp = 0f;

        while (audioS.volume >= 0f)
        {
            audioS.volume = Mathf.Lerp(0.8f, 0f, temp);
            yield return null;
            temp += Time.deltaTime;

            if (audioS.volume <= 0f)
            {
                StopCoroutine(IEnumFadeOut());
                yield return null;
            }
        }
        
    }

    private void FadeIn()
    {
        if (audioS)
        {
            StartCoroutine(IEnumFadeIn());
        }
#if UNITY_EDITOR
        else
        {
            throw new NullReferenceException(nameof(MusicManager).ToUpper() + ": Missing Audio Source!");
        }
#endif
    }

    private void FadeOut()
    {
        if (audioS)
        {
            StartCoroutine(IEnumFadeOut());
        }
#if UNITY_EDITOR
        else
        {
            throw new NullReferenceException(nameof(MusicManager).ToUpper() + ": Missing Audio Source!");
        }
#endif
    }

    public void Pause() => audioS.Pause();

    public void UnPause() => audioS.UnPause();

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
#if UNITY_EDITOR
            DestroyImmediate(gameObject);
#else
            Destroy(gameObject);
#endif
        }
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        Instance = null;
    }
#endif
    private void OnApplicationQuit()
    {
        Instance = null;
    }

    public void SceneUtil_OnActivation()
    {
        StopAllCoroutines();

        Init();
        SetMusic();
        FadeIn();
    }

    public void SceneUtil_LoadNextScene()
    {
        FadeOut();
    }

    private void Init()
    {
        if (audioS)
        {
            audioS.loop = true;
            audioS.volume = 0;
        }
        else
        {
            throw new NullReferenceException(nameof(MusicManager) + ": AUDIO SOURCE IS MISSING!");
        }
    }

    private void SetMusic()
    {
        string scName = SceneManager.GetActiveScene().name;

        if (scName == "CreditsScene")
        {
            audioS.clip = null;
            return;
        }


        if (!audioS)
            throw new NullReferenceException(nameof(MusicManager) + " : Missing AudioSource!");
        
           
        AudioClip clip = null;

        switch (scName)
        {
            case "PreloaderScene":
                clip = preloaderClip;
                break;

            case "MainMenuScene":
                clip = mainMenuClip;
                break;

            default: //Plays a Level Music

                string sub = scName.Substring(scName.Length - 1);

                for (int i = 0; i < levelMusicClips.Length; i++)
                {
                    print(sub + " | " + i.ToString());
                    if (sub == i.ToString())
                    {
                        clip = levelMusicClips[i];
                        Debug.Log(nameof(MusicManager) + ": Music found!");
                        break;
                    }
                }

                if(!clip)
                    throw new NullReferenceException(nameof(MusicManager).ToUpper() + ": Music not found!");

                Debug.Log(nameof(MusicManager).ToUpper() + ": Playing Level " + sub + " Music");

                break;
        }

        audioS.clip = clip;
        audioS.Play();
    }
}
