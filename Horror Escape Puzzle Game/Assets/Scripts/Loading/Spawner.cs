
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D), typeof(AudioSource))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject playerObj = null;
    [SerializeField] private bool init = false;
    [SerializeField] private bool playerHasVCam = false;
    [SerializeField] private bool useGameManager = true;
    private void Start()
    {
        if(init)
        {
            SpawnPlayer();
            GameManager.Instance.CurrentSpawnPoint = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (useGameManager)
        {
            if(GameManager.Instance && collision.CompareTag("Player"))
            {
                if(GameManager.Instance.CurrentSpawnPoint != this && !init)
                {
                    GameManager.Instance.CurrentSpawnPoint = this;

                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    public void SpawnPlayer() 
    {
        Vector3 position = transform.position + new Vector3( 0f, 0.5f);

        if (playerHasVCam)
        {
            playerObj.GetComponent<Player_Behavior>().HasVCam = true;
        }

        Instantiate(playerObj, position , transform.rotation).transform.parent = null;

    }

}
