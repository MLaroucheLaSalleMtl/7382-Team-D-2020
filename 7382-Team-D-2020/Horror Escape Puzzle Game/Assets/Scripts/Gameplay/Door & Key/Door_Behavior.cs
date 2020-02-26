using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Door_Behavior : MonoBehaviour
{

    [Header("Place the Gameobject \"Key\" that goes with this Door")]
    [SerializeField] private Key_Behavior key;

    public void Open()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (key.IsPickedUp) Open();
    }
}
