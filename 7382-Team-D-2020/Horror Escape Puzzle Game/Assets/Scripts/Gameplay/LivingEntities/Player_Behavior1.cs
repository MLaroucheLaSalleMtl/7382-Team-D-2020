
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

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Behavior1 : MonoBehaviour
{
    private UnityEvent OnDeath;

    private GameMenuManager gmm;
    private Rigidbody2D rigid;

    private Vector2 movement;

    [SerializeField] private float speed; //default of 4
    [SerializeField] private float upwardsVelocity; //default of 5

    private bool canJump = true;

    private void Awake()
    {
        OnDeath = new UnityEvent();

        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gmm = GameMenuManager.GetInstance;
        OnDeath.AddListener(gmm.OnPlayerDeath);
    }
    private void FixedUpdate()
    {
        rigid.AddForce(transform.right * speed * movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ground")) Death();
        else canJump = true;
    }

    public void Jump()
    {
        if (canJump)
        {
            rigid.AddForce(transform.up * upwardsVelocity, ForceMode2D.Impulse);
            canJump = false;
        }
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


    public void SetMovement(Vector2 direction) => movement = direction;

}
