
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Shaky_Platform : MonoBehaviour
{
    private BoxCollider2D coll;
    private Rigidbody2D rigid;

    private Vector2 initPosition;

    [Tooltip("The time take takes before the platform drops.")]
    [SerializeField] private float delayTime;

    [Range(1f,10f)]
    [SerializeField] private float gravity;

    [SerializeField] private float respawnTimer = 5f;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        coll.enabled = true;

        initPosition = transform.position;

        rigid.bodyType = RigidbodyType2D.Static; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("Drop", delayTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("Drop", delayTime);
        }

    }

    private void Drop()
    {
        float angularForce;

        if (Random.Range(0,2) == 0)
        {
            angularForce = Random.Range(-10f, -5f);
        }
        else
        {
            angularForce = Random.Range(5f, 10f);
        }

        rigid.bodyType = RigidbodyType2D.Dynamic;
        rigid.gravityScale = gravity;
        rigid.angularVelocity = angularForce;
        coll.enabled = false;

        Invoke("Delete", respawnTimer);
    }

    private void Delete()
    {
        Instantiate(gameObject, initPosition, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
