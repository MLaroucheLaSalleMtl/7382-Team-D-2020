using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Spike_Behavior : MonoBehaviour
{

    private Animator anim;

    private void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        anim.SetTrigger("Enter");
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetTrigger("Exit");
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Behavior>().Death();
        }
        else if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster_Behavior>().Death();
        }
    }


}
