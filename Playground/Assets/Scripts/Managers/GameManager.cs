
using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameState, ISceneUtility
{
    public static GameManager Instance = null;

    private bool isGamePaused = false;
    private bool sceneIsLevel = false;

    [HideInInspector]public Spawner CurrentSpawnPoint;

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        AddListeners();
    }

    /// <summary>
    /// Check if game is currently paused.
    /// </summary>
    /// <return>
    /// Returns true or false.
    /// </return>>
    public bool IsGamePaused { get => isGamePaused; }

    public void OnPlayerDeath()
    {
        GameMenuManager.Instance.OnPlayerDeath();

        StartCoroutine(WaitForSeconds());
    }

    public void GamePauseToggler()
    {
        if (!sceneIsLevel)
            return;

        if (isGamePaused)
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
        
        Debug.Log(nameof(GameManager) + ": Game Paused" );
        GameMenuManager.Instance.Pause();
        MusicManager.Instance.Pause();
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void OnPlayerRespawn()
    {
        GameMenuManager.Instance.UnPauseTimer();
    }

    /// <summary>
    /// Unpauses the game.
    /// </summary>
    public void UnPause()
    {
        Debug.Log(nameof(GameManager) + ": Game UnPause " + (GameMenuManager.Instance is null).ToString());

        GameMenuManager.Instance.UnPause();
        MusicManager.Instance.UnPause();

        isGamePaused = false;
        Time.timeScale = 1f;
    }

    private void AddListeners()
    {
        if (Controls.Instance != null)
        {
            Controls.Instance.UAction_OnEscapePress += GamePauseToggler;
        }
    }

    private void RemoveListeners()
    {
        if (Controls.Instance != null)
        {
            Controls.Instance.UAction_OnEscapePress -= GamePauseToggler;
        }
    }

    public void DestorySelf()
    {
#if UNITY_EDITOR
        DestroyImmediate(gameObject);
#else
            Destroy(gameObject);
#endif
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

        CurrentSpawnPoint = null;
        UnPause();
        Check_SceneIsLevel();
    }

    private void Check_SceneIsLevel()
    {
        switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            case "PreloaderScene":
            case "MainMenuScene":
            case "CreditsScene":
                sceneIsLevel = false;
                break;

            default:
                sceneIsLevel = true;
                break;
        }
    }

    public void SceneUtil_LoadNextScene()
    {
        // Nothing needs to be done for now
    }
    
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;
            
    }

    private IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(Settings.DeathWaitTimer);
        CurrentSpawnPoint.SpawnPlayer();
    }

}

