using UnityEngine;

public class GameobjectScaler : MonoBehaviour
{
    private Transform obj;

    private void Awake()
    {
        obj = GetComponent<Transform>();
    }

    public void SetScale0()
    {
        obj.localScale = Vector3.zero;
    }

    public void SetScale1()
    {
        obj.localScale = Vector3.one;
    }

}
