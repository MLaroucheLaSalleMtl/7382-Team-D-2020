

using UnityEngine;

public class GameManager : MonoBehaviour, IGameState
{

    public static GameManager instance = null;

    private GameMenuManager gmm = null;
    private MusicManager mm = null;
    private bool isGamePaused = false;

    [HideInInspector]public Spawner currentSpawnPoint;

    private void Awake()
    { 
        CreateSingleton();

        gmm = GameMenuManager.instance;
        mm = MusicManager.instance;
    }
    
    /// <summary>
    /// Check if game is currently paused.
    /// </summary>
    /// <return>
    /// Returns true or false.
    /// </return>>
    public bool IsGamePaused { get; set; }

    public void OnPlayerDeath()
    {
        GameMenuManager.instance.OnPlayerDeath();
        currentSpawnPoint.SpawnPlayer();
    }

    public void ChangeGameState()
    {
        if (IsGamePaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void Pause()
    {
        Debug.Log("Pause");
        gmm.Pause();
        mm.Pause();
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Unpauses the game.
    /// </summary>
    public void UnPause()
    {
        Debug.Log("UnPause");
        gmm.UnPause();
        mm.UnPause();
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void SetSpawnAsNull() => currentSpawnPoint = null;

    private void CreateSingleton()
    {
        if (instance == null)
        {
            instance = this;

            PlayerData.NumGamOpentime++;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnApplicationQuit()
    {
        instance = null;
    }
}

