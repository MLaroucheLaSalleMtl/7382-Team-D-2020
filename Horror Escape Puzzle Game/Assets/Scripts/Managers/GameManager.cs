
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    private bool isGamePaused = false;

    [HideInInspector]public Spawner currentSpawnPoint;



    private void Awake()
    { 
        CreateSingleton();
        
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        ResumeGame();

    }


    /// <summary>
    /// Check if game is currently paused.
    /// </summary>
    /// <return>
    /// Returns true or false.
    /// </return>>
    public bool IsGamePaused => isGamePaused;

    public void RespawnPlayer()
    {

        GameMenuManager.instance.OnPlayerDeath();
        currentSpawnPoint?.SpawnPlayer();
        
    }
    private void CreateSingleton()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame() => Time.timeScale = 0f;
    public void ResumeGame() => Time.timeScale = 1f;

    private void OnDestroy()
    {
        instance = null;
    }
}

