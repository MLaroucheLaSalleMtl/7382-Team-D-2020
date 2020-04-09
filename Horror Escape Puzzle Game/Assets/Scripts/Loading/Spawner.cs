
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private bool init = false;
    [SerializeField] private bool playerHasVCam = false;
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
        Vector3 position = transform.position + new Vector3( 0f, 0.5f);

        if (playerHasVCam) playerObj.GetComponent<Player_Behavior>().HasVCam = true;
        Instantiate(playerObj, position , transform.rotation).transform.parent = null;
    }

}
