using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    //private Camera_Behavior cb;
    private bool isGamePaused = false;
    UnityEvent OnPlayerRespawn = new UnityEvent();


    private void Awake()
    {
        if (!(instance is null) && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    } //Singleton

    private void Start()
    {
        //cb = Camera_Behavior.GetInstance;

        //OnPlayerRespawn.AddListener(cb.FindandAttachOnPlayer);
    }

    private void OnDestroy()
    {
        instance = null;
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
        if (SaveLoadCheckpoint.GetLastCheckpoint != null)
        {
            SaveLoadCheckpoint.GetLastCheckpoint.gameObject.GetComponent<SaveLoadCheckpoint>().Respawn();
            OnPlayerRespawn.Invoke();
        }
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame() => Time.timeScale = 0f;
    public void ResumeGame() => Time.timeScale = 1f;
    public static GameManager GetInstance => instance;
}
