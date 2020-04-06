
using UnityEngine;
using UnityEngine.Events;

public static class Player
{

    private static int life = 5; //default 5 lifes per session, num can go negative

    public static int Life
    {
        get => life;
        set => life = value > life ? life : value;
    }
}

public class Player_Behavior : MonoBehaviour
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
        gmm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMenuManager>();
        OnDeath.AddListener(gmm.OnPlayerDeath);
        OnLand.AddListener(this.gameObject.GetComponent<PlayerInputs>().HasJumped);
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
        Debug.Log("player death");
        //if only collision with spikes - gets stuck at that position and piss blood
        //if trigger enter on fire/lava/laser - gets fried to a crisp
        //if bomb vest explodes - blood flies everywhere + screen shake?
        Player.Life--;
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        OnDeath?.Invoke();
    }
}
