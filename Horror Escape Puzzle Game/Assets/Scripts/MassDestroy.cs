
using UnityEngine;

public class MassDestroy : MonoBehaviour
{
    [SerializeField] private GameObject[] objsTobeCleansed;

    public void Cleanse()
    {
        foreach(GameObject obj in objsTobeCleansed) Destroy(obj);
        Destroy(gameObject);
    }

}
