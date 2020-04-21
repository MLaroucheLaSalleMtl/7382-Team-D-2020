
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Spike_Behavior : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) PlayerData.DeathBySpikes++; 
    }
}
