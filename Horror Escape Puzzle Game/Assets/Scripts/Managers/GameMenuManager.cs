
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathScreen;
     private GameManager gm;

    public static GameMenuManager instance = null;

    private void Awake()
    {
        CreateSingleton();
    } 

    void Start()
    {
        gm = GetComponent<GameManager>();

        pauseMenu.SetActive(false);
    }

    private void OnEscapeToggle(InputAction.CallbackContext context)
    {
        if (context.performed && gm.IsGamePaused)
        {
            gm.PauseGame();
            pauseMenu.SetActive(true);
        }
        else
        {
            gm.ResumeGame();
            pauseMenu.SetActive(false);
        }
    }

    public void OnPlayerDeath()
    {
        deathScreen?.SetActive(true);   
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
    private void OnDestroy()
    {
        instance = null;
    }
}
