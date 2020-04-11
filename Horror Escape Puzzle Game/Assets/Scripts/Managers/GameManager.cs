

using UnityEngine;

public class GameManager : MonoBehaviour, IGameState
{

    public static GameManager instance = null;

    private GameMenuManager gmm = null;

    private bool isGamePaused = false;

    [HideInInspector]public Spawner currentSpawnPoint;

    private void Awake()
    { 
        CreateSingleton();
        
        DontDestroyOnLoad(gameObject);

        Debug.Log("GM: " + instance);
    }

    private void Start()
    {
        gmm = GameMenuManager.instance;
        Play();
    }

    public void SetSpawnAsNull() => currentSpawnPoint = null;

    
    /// <summary>
    /// Check if game is currently paused.
    /// </summary>
    /// <return>
    /// Returns true or false.
    /// </return>>
    public bool IsGamePaused { get => isGamePaused; set => isGamePaused = value; }

    public void OnPlayerDeath()
    {
        GameMenuManager.instance.OnPlayerDeath();
        currentSpawnPoint.SpawnPlayer();
    }
    private void CreateSingleton()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void Pause()
    {
        gmm.Pause();
        Time.timeScale = 0f;
    }
    public void Play()
    {
        gmm.Play();
        Time.timeScale = 1f;
    }
    private void OnDestroy()
    {
        instance = null;
    }
}

