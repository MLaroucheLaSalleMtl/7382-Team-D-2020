
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.GetComponent<Player_Behavior>().Death();
        if (collision.CompareTag("TestDummy")) collision.GetComponent<Player_Behavior1>().Death();
    }
}
