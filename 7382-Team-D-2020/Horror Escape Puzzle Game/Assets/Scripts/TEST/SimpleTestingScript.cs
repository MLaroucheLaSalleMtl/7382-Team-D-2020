using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTestingScript : MonoBehaviour
{
    //only used for testing 
    private float speed = 3f;
    private void Update()
    {
        if (Input.GetKey(KeyCode.A)) this.transform.Translate(Vector2.left * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D)) this.transform.Translate(Vector2.right * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.W)) this.transform.Translate(Vector2.up * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.S)) this.transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Space)) this.GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Impulse);
    }
}
