
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Shaky_Platform : MonoBehaviour
{
    private BoxCollider2D coll = null;
    private Rigidbody2D rigid = null;

    private Vector2 initPosition = Vector2.zero;

    [Tooltip("The time take takes before the platform drops.")]
    [SerializeField] private float delayTime = 0f;

    [Range(1f,10f)]
    [SerializeField] private float gravity = 1f;

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
            Invoke(nameof(Drop), delayTime);
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

        Invoke(nameof(Delete), respawnTimer);
    }

    private void Delete()
    {
        Instantiate(gameObject, initPosition, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
