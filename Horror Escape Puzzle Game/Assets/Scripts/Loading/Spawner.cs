
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private bool init = false;
    [SerializeField] private bool playerHasVCam = false;
    [SerializeField] private bool useGameManager = true;
    private void Start()
    {
        if(init) SpawnPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (useGameManager)
        {
            GameManager gm = GameManager.instance;
            if (collision.CompareTag("Player")) gm.currentSpawnPoint = this;
        }
    }

    public void SpawnPlayer() 
    {
        Vector3 position = transform.position + new Vector3( 0f, 0.5f);

        if (playerHasVCam) playerObj.GetComponent<Player_Behavior>().HasVCam = true;
        Instantiate(playerObj, position , transform.rotation).transform.parent = null;
    }

}
