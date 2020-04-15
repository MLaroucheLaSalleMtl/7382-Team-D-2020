
using UnityEngine;

public class DeathZoneCounter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) PlayerData.DeathByFall++;
    }
}
