
using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond = 0f;
    private void Update()
    {
        transform.Rotate(Vector3.forward, degreesPerSecond * Time.deltaTime) ;
    }
}
