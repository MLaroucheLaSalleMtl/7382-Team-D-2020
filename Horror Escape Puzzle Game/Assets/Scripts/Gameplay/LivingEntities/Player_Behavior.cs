using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player_Behavior: MonoBehaviour
{
    private UnityEvent OnDeath = new UnityEvent();

    private Rigidbody2D rigid;

    private Vector2 movement;

    private SpriteRenderer sprt;

    [SerializeField] private float speed = 4; 
    [SerializeField] private float upwardsVelocity = 3.5f;

    public bool HasVCam = false;

    private int surfaceContact = 0;

    [SerializeField] private AudioClip[] deathSFX;
    [SerializeField] private AudioClip[] respawnSFX;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprt = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if(GameManager.instance != null) OnDeath.AddListener(GameManager.instance.OnPlayerDeath);
        if(HasVCam) GetCinemachineVCam();

        PlayRespawnSFX();
    }

    private void PlayRespawnSFX()
    {
        GetComponent<AudioSource>().clip = respawnSFX[UnityEngine.Random.Range(0, respawnSFX.Length)];
        GetComponent<AudioSource>().Play();
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
            surfaceContact++;
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") )
        {
            surfaceContact--;
            if(surfaceContact < 1)
            {
                sprt.enabled = false;
            }
        }
    }

    public void Jump()
    {
        if (CanJump)
        {
            rigid.AddForce(transform.up * upwardsVelocity, ForceMode2D.Impulse);
        }
    }

    private bool CanJump => surfaceContact > 0;

    public void Death()
    {
        StartCoroutine(WaitForSeconds());
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator WaitForSeconds()
    {
        GetComponent<AudioSource>().clip = deathSFX[UnityEngine.Random.Range(0, deathSFX.Length)];
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
    }

    public void SetMovement(Vector2 direction) => movement = direction;

}
