﻿
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Slamming_Wall : MonoBehaviour
{

    //Choose in which direction to slam the wall
    public enum Directions { Up, Down, Left, Right }
    public Directions orientation;

    [HideInInspector] private Vector2 v2Direction = Vector2.zero;

    [HideInInspector] private Vector2 originalPos = Vector2.zero;

    [Range(1f, 5f)] 
    [SerializeField] private float speed = 0f;

    [SerializeField] private float attackSpeed = 10f;

    [Header("Reset Position Timer")]
    [Tooltip("Time that takes to call the reset position function.")]
    [Range(0f, 5f)] 
    [SerializeField] private float resetTimer = 0f;


    private bool hasPrey = false;
    private Rigidbody2D rigid = null;

    // Start is called before the first frame update
    private void Start()
    {
        //Saves starting position
        originalPos = this.gameObject.transform.position;
        if(rigid) GetComponent<Rigidbody2D>();
        hasPrey = false;

        //Picks the right direction basically this forces the gameobject to go one direction
        switch (orientation)
        {
            case Directions.Up:
                v2Direction = Vector2.up;
                break;

            case Directions.Down:
                v2Direction = Vector2.down;
                break;

            case Directions.Left:
                v2Direction = Vector2.left;
                break;

            case Directions.Right:
                v2Direction = Vector2.right;
                break;
        }
    }


    private void FixedUpdate()
    {
        if (hasPrey)
        {
            AttackPrey();
        }
        else
        {
            ResetPosition();
        }
    }

    private void AttackPrey()
    {
        Debug.Log("Attacking Prey");
        rigid.AddForce(v2Direction * attackSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Prey");
        if(collision.CompareTag("Player")) hasPrey = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("No Prey");
        if(collision.CompareTag("Player")) hasPrey = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigid.velocity = Vector2.zero;
        Invoke(nameof(ResetPosition), resetTimer);
    }

    private void ResetPosition()
    {
        Vector2 direction = (Vector2)this.gameObject.transform.position - originalPos;
        this.gameObject.transform.Translate(direction * speed * Time.deltaTime);
    }

    public Vector2 Direction => this.v2Direction;

}