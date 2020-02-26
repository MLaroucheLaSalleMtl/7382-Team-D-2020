using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Behavior : MonoBehaviour
{

    [HideInInspector] private bool isPickedUp;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
        isPickedUp = true;
    }

    public bool IsPickedUp => isPickedUp;
}
