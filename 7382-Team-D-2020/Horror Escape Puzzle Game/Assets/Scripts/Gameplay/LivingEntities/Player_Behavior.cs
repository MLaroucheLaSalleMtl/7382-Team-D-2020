
using UnityEngine;
using UnityEngine.Events;

public class Player_Behavior : MonoBehaviour, IDeath
{
    private UnityEvent OnDeath;
    [HideInInspector] public UnityEvent OnLand;
    private GameMenuManager gmm;

    private RaycastHit2D rayRight, rayLeft;

    //Not Sure if to be kept
    private bool isRightWall, isLeftWall;

    private void Awake()
    {
        OnDeath = new UnityEvent();
        OnLand = new UnityEvent();

    }

    private void Start()
    {
        gmm = GameMenuManager.GetInstance;
        OnDeath.AddListener(gmm.OnPlayerDeath);
        OnLand.AddListener(this.gameObject.GetComponent<PlayerInputs>().HasJumped);
    }

    private void Update()
    {
        CheckCollisionRight();
        CheckCollisionLeft();
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

    private void CheckCollisionRight()
    {
        rayRight = Physics2D.Raycast(this.gameObject.transform.position, Vector2.right * 2);

        if (rayRight.collider.CompareTag("Ground")) { isRightWall = true; Debug.Log("Colli right"); }
        else isRightWall = false;
    }

    private void CheckCollisionLeft()
    {
        rayLeft = Physics2D.Raycast(this.gameObject.transform.position, Vector2.left * 2);

        if (rayLeft.collider.CompareTag("Ground")) { isLeftWall = true; Debug.Log("Colli left"); }
        else isLeftWall = false;
    }

    public void Death()
    {
        Debug.Log("player death");
        //if only collision with spikes - gets stuck at that position and piss blood
        //if trigger enter on fire/lava/laser - gets fried to a crisp
        //if bomb vest explodes - blood flies everywhere + screen shake?
        CurrentSessionPlayerData.Life--;
        OnDeath.Invoke();
        Destroy(this.gameObject);

    }

    //private void OnDestroy()
    //{
        
    //    //BUG: Might cause to have 2 players and cameras on the screen;
         // it is not might lol, it is it does cause random player to be spawned due to event
    //}
}
