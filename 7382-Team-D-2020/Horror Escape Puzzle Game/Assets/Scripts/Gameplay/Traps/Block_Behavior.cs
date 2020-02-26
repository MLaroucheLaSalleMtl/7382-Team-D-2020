using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Block_Behavior : MonoBehaviour
{
    [Header("Set Block visibility")]
    [SerializeField] private bool startVisible;

    [Tooltip("Number of times before the trap gets triggered")]
    [SerializeField] private int triggerCounter;
    private int counter;

    [HideInInspector] private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (!startVisible) sprite.enabled = false;
        else sprite.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (counter == triggerCounter)
        {
            if (startVisible)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                sprite.enabled = true;
                this.gameObject.SetActive(true);
            }
        }
        else
        {
            counter++;
        }
        
    }

    //Can make a trigger for this too;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        counter++;
    }
}
