
using UnityEngine;
using UnityEngine.Events;

public static class Player1
{

    private static int life = 5; //default 5 lifes per session, num can go negative

    public static int Life
    {
        get => life;
        set => life = value > life ? life : value;
    }
}

public class Player_Behavior1 : MonoBehaviour
{
    private UnityEvent OnDeath;
    [HideInInspector] public UnityEvent OnLand;
    private GameMenuManager gmm;

    private void Awake()
    {
        OnDeath = new UnityEvent();
        OnLand = new UnityEvent();
    }

    private void Start()
    {
        gmm = GameMenuManager.GetInstance;
        OnDeath.AddListener(gmm.OnPlayerDeath);
        OnLand.AddListener(this.gameObject.GetComponent<PlayerInputs1>().HasJumped);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ground")) Death();
        else Land();
    }

    public void Land()
    {
        OnLand?.Invoke();
    }

    public void Death()
    {
        Player.Life--;
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        OnDeath?.Invoke();
    }
}
