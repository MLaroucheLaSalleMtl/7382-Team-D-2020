
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

        if(GetComponent<GameMenuManager>()) gmm = GetComponent<GameMenuManager>();
        else Debug.Log("Menu Manager is Missing");
        if (MusicManager.instance) mm = MusicManager.instance;
        else Debug.Log("Music Manager is Missing");
    }
    
    /// <summary>
    /// Check if game is currently paused.
    /// </summary>
    /// <return>
    /// Returns true or false.
    /// </return>>
    public bool IsGamePaused { get => isGamePaused; set=> isGamePaused = value; }

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
        IsGamePaused = true;
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
        IsGamePaused = false;
        Time.timeScale = 1f;
    }

    public void SetSpawnAsNull() => currentSpawnPoint = null;

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

#if UNITY_EDITOR
    private void OnDestroy()
    {
        PlayerData.SaveData();
    }
#endif

    private void OnApplicationQuit()
    {
        instance = null;
    }
}

