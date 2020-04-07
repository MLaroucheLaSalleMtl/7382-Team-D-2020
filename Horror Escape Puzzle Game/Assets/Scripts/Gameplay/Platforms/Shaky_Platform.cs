using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Shaky_Platform : MonoBehaviour
{
    private BoxCollider2D coll;
    private Rigidbody2D rigid;

    [Tooltip("The time take takes before the platform drops.")]
    [SerializeField] private float delayTime;

    [Range(1f,10f)]
    [SerializeField] private float gravity;


    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        rigid.bodyType = RigidbodyType2D.Static; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("Drop", delayTime);
    }

    private void Drop()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        rigid.gravityScale = gravity;
        rigid.angularVelocity = Random.Range(-180f, 180f);
        coll.enabled = false;

        Invoke("Delete", 3f);
    }

    private void Delete()
    {
        Destroy(this.gameObject);
    }

}
