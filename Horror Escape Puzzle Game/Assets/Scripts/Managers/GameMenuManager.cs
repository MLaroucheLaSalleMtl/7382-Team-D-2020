
using UnityEngine;

public class GameMenuManager : MonoBehaviour, IGameState
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathScreen;

    public static GameMenuManager instance = null;
    private Timer timer = null;

    private void Awake()
    {
        CreateSingleton();
    } 

    private void Start()
    {
        GameObject timerObj = GameObject.FindGameObjectWithTag("Timer");

        if (timerObj != null) timer = timerObj.GetComponent<Timer>();

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
        deathScreen?.SetActive(true);   
    }
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

    public void Pause()
    {
        PauseTimer();
    }

    public void UnPause()
    {
        StartTimer();
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
