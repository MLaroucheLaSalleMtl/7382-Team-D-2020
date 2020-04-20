
using UnityEngine;

public class Arrow_Behavior : MonoBehaviour
{
    [HideInInspector, SerializeField]private float speed = 0f;
    [HideInInspector, SerializeField]public bool Homing = false;

    private Transform trackedObjPos;
    private Rigidbody2D rigid;

    [SerializeField] private float degreePerSecond = 180f;

    public float Speed { set => speed = value; }

    [SerializeField] private GameObject speed_VFX = null;
    [SerializeField] private ParticleSystem onHit_VFX = null;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (Homing) trackedObjPos = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        if (Homing) HomingToTarget();
        else
        {
            if (rigid != null) rigid.velocity = this.gameObject.transform.up * this.speed * 100 * Time.deltaTime;
        } 
    }

    private void HomingToTarget()
    {
        if(trackedObjPos != null)
        {
            Vector2 direction = trackedObjPos.position - this.transform.position;

            float rotation = Vector3.Cross(direction.normalized, transform.up).z;
            rigid.angularVelocity = -rotation * degreePerSecond;
            rigid.velocity = transform.up * this.speed * 100f * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Trap"))
        {
            Invoke(nameof(DeleteGameobject), 3f);
            Destroy(rigid);
            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<TrailRenderer>());
            Homing = false;

            speed_VFX.SetActive(false);
            onHit_VFX.Play();
        }

        if (collision.collider.CompareTag("Player")) PlayerData.DeathByArrow++;
    }

    private void DeleteGameobject()
    {
        Destroy(this.gameObject);
    }
}
