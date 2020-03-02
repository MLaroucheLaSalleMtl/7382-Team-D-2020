
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Arrow_Behavior : MonoBehaviour
{
    [HideInInspector][SerializeField] private float speed;

    [HideInInspector] [SerializeField] private bool homing = false;

    private Transform trackedObjPos;
    private Rigidbody2D rigid;
    private Collider2D coll;

    public float Speed { set => speed = value; }
    public bool Homing { get => homing;  set => homing = value; }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if (homing) trackedObjPos = GameObject.FindGameObjectWithTag("Player").transform;
        
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        if (homing) HomingToTarget();
        else if (rigid != null)
        {
            rigid.velocity = this.gameObject.transform.up * this.speed * 100 * Time.deltaTime;
        } 
    }


    private void HomingToTarget()
    {
        Vector2 direction = trackedObjPos.position - this.transform.position;
      
        float rotation = Vector3.Cross(direction.normalized, transform.up).z;
        rigid.angularVelocity = -rotation * 360f ;
        rigid.velocity = direction.normalized * this.speed * 100f * Time.deltaTime;
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT!");
            collision.gameObject.transform.parent = this.gameObject.transform;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().mass = 0f;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = this.gameObject.GetComponent<Rigidbody2D>().velocity;
            collision.GetComponent<Player_Behavior>().Death();
        }
        else
        {
            Destroy(rigid);
            Destroy(this);
            Destroy(coll);
        }
    }

    
}
