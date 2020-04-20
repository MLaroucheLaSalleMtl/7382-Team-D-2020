
using UnityEngine;

public class GameMenuManager : MonoBehaviour, IGameState, ISceneUtility
{

    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject deathScreen = null;

    public static GameMenuManager Instance = null;
    private Timer timer = null;

    private void Awake()
    {
        CreateSingleton();
    } 

    private void StartTimer()
    {
        if (timer != null)
        {
            timer.Enable();
        }
    }

    private void PauseTimer()
    {
        if (timer != null)
        {
            timer.Paused = true;
        }
    }
    public void OnPlayerDeath()
    {
        PauseTimer();
        if(deathScreen) deathScreen.SetActive(true);   
    }
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

    public void Pause()
    {
        PauseTimer();
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        StartTimer();
        pauseMenu.SetActive(false);
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

        GameObject timerObj = GameObject.FindGameObjectWithTag("Timer");

        if (timerObj) timer = timerObj.GetComponent<Timer>();

        pauseMenu.SetActive(false);
    }

    public void SceneUtil_LoadNextScene()
    {
        // Nothing needs to be done for now
    }
}
