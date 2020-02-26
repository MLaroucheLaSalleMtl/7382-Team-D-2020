
using UnityEngine;


//If you are looking for SaveLoad script, it is this one
// I've changed a few things

[RequireComponent(typeof(EdgeCollider2D))]
public class SaveLoadCheckpoint : MonoBehaviour
{
    private static Transform lastCheckpointPos;

    [SerializeField] private GameObject playerObj;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        if (collision.CompareTag("Player")) lastCheckpointPos = this.gameObject.transform; 
    }

    public void Respawn()
    {
        if (gameObject.transform.position.Equals(lastCheckpointPos.position))
        {
            Instantiate(playerObj, transform.position, transform.rotation).transform.parent = null;
        }
    }

    private void OnDestroy()
    {
        Destroy(this.gameObject);
    }
    public static Transform GetLastCheckpoint => lastCheckpointPos;

}
