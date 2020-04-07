
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private bool init = false;
    private void Start()
    {
        if(init) SpawnPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) GameManager.instance.currentSpawnPoint = this;
    }

    public void SpawnPlayer() 
    {
        Instantiate(playerObj, transform.position, transform.rotation).transform.parent = null;
    }

}
