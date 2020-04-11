using Cinemachine;
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

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player_Behavior: MonoBehaviour
{
    private UnityEvent OnDeath = new UnityEvent();

    private Rigidbody2D rigid;

    private Vector2 movement;

    private SpriteRenderer sprt;

    [SerializeField] private float speed = 4; 
    [SerializeField] private float upwardsVelocity = 5;

    public bool HasVCam = false;

    private bool canJump = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprt = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if(GameManager.instance != null) OnDeath.AddListener(GameManager.instance.OnPlayerDeath);
        if(HasVCam) GetCinemachineVCam();
    }

    public void GetCinemachineVCam()
    {
        GameObject gobj = GameObject.FindGameObjectWithTag("VCam");
        CinemachineVirtualCamera vcam = gobj?.GetComponent<CinemachineVirtualCamera>();
        if(vcam != null) vcam.Follow = this.gameObject.transform;
    }

    private void FixedUpdate()
    {
        rigid.AddForce(transform.right * speed * movement.normalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ground"))
        {
            Death();
        }
        else
        {
            canJump = true;
            sprt.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            Death();
        }
    }

    public void Jump()
    {
        if (canJump)
        {
            rigid.AddForce(transform.up * upwardsVelocity, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground")) sprt.enabled = false;
    }

    public void Death()
    {
        Player.Life--;
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void SetMovement(Vector2 direction) => movement = direction;

}
