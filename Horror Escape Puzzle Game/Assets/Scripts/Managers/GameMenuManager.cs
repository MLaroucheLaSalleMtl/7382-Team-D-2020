
using UnityEngine;

public class GameMenuManager : MonoBehaviour, IGameState
{

    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject deathScreen = null;

    public static GameMenuManager instance = null;
    private Timer timer = null;

    private void Awake()
    {
        CreateSingleton();
    } 

    private void Start()
    {
        GameObject timerObj = GameObject.FindGameObjectWithTag("Timer");

        if (timerObj) timer = timerObj.GetComponent<Timer>();

        pauseMenu.SetActive(false);
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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
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

    private void OnApplicationQuit()
    {
        instance = null;
    }
}
