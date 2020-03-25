
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class SaveLoadCheckpoint : MonoBehaviour
{
    private static Transform lastCheckpointPos;

    [SerializeField] private GameObject playerObj;

    private void Start()
    {
        SpawnPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) lastCheckpointPos = this.gameObject.transform;
        if (collision.CompareTag("TestDummy")) lastCheckpointPos = this.gameObject.transform;
    }

    public void Respawn()
    {
        if (gameObject.transform.position.Equals(lastCheckpointPos.position))
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer() 
    { 
        Instantiate(playerObj, transform.position, transform.rotation).transform.parent = null;
    }

    public static Transform GetLastCheckpoint => lastCheckpointPos;

}
