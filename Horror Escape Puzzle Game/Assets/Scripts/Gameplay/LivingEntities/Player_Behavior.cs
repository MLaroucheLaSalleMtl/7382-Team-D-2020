using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player_Behavior: MonoBehaviour
{
    private UnityEvent OnDeath = new UnityEvent();

    //Player
    private SpriteRenderer sprt = null;
    private Rigidbody2D rigid = null;
    private Vector2 movement = Vector2.zero;
    [SerializeField] private float speed = 5;
    [SerializeField] private float upwardsVelocity = 6.5f;
    public bool HasVCam = false;
    private int surfaceContact = 0;

    //SFX
    [SerializeField] private AudioClip[] deathSFX = null;
    [SerializeField] private AudioClip[] respawnSFX = null;
    private AudioSource audioS = null;
    private void Awake()
    {
        if(GetComponent<Rigidbody2D>()) rigid = GetComponent<Rigidbody2D>();
        if(GetComponent<SpriteRenderer>()) sprt = GetComponent<SpriteRenderer>();
        if(GetComponent<AudioSource>()) audioS = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(GameManager.instance != null) OnDeath.AddListener(GameManager.instance.OnPlayerDeath);
        if(HasVCam) GetCinemachineVCam();

        PlayRespawnSFX();
    }

    private void PlayRespawnSFX()
    {
        if (audioS)
        {
            Debug.Log("respawn SFX");
            audioS.clip = respawnSFX[Random.Range(0, respawnSFX.Length)];
            audioS.Play();
        }
    }

    public void GetCinemachineVCam()
    {
        GameObject gobj = GameObject.FindGameObjectWithTag("VCam");
        CinemachineVirtualCamera vcam = gobj?.GetComponent<CinemachineVirtualCamera>();
        if(vcam) vcam.Follow = this.gameObject.transform;
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
        Destroy(gameObject, 0.5f);
    }

    private IEnumerator WaitForSeconds()
    {
        Debug.Log("Death SFX");
        GetComponent<AudioSource>().clip = deathSFX[Random.Range(0, deathSFX.Length)];
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
    }

    public void SetMovement(Vector2 direction) => movement = direction;

}
