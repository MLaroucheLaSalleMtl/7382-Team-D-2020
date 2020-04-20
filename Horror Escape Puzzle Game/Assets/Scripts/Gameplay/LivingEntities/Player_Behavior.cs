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
        if(GameManager.Instance != null) OnDeath.AddListener(GameManager.Instance.OnPlayerDeath);
        if(HasVCam) GetCinemachineVCam();

        AddListeners();

        PlayRespawnSFX();
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

        if (collision.CompareTag("Teleport"))
        {
            gameObject.layer = 12; // Ground collision only
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

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void PlayRespawnSFX()
    {
        if (audioS)
        {
            Debug.Log("respawn SFX");
            audioS.clip = respawnSFX[UnityEngine.Random.Range(0, respawnSFX.Length)];
            audioS.Play();
        }
    }

    public void GetCinemachineVCam()
    {
        GameObject gObj = GameObject.FindGameObjectWithTag("VCam");
        if (gObj) gObj.GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform;
    }

    private void Jump()
    {
        if (CanJump)
        {
            rigid.AddForce(transform.up * upwardsVelocity, ForceMode2D.Impulse);
        }
    }

    private bool CanJump => surfaceContact > 0;

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false; // Prevents the player to die multiple times and bug out
        StartCoroutine(WaitForSeconds());
        OnDeath?.Invoke();
        Destroy(gameObject, 0.5f);
    }

    private IEnumerator WaitForSeconds()
    {
        Debug.Log("Death SFX");
        GetComponent<AudioSource>().clip = deathSFX[UnityEngine.Random.Range(0, deathSFX.Length)];
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
    }

    private void Move(Vector2 direction) => movement = direction;

    private void AddListeners()
    {
        Controls ctrls = Controls.Instance;

        if (ctrls)
        {
            ctrls.UAction_OnJump += Jump;
            ctrls.UAction_OnMove += Move;
        }
    }

    private void RemoveListeners()
    {
        Controls ctrls = Controls.Instance;

        if (ctrls)
        {
            ctrls.UAction_OnJump -= Jump;
            ctrls.UAction_OnMove -= Move;
        }
    }
}
