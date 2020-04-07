using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles_Behavior : MonoBehaviour
{

    [SerializeField] private float speedDirection;

    void Update()
    {
        gameObject.transform.Rotate(0, 0, speedDirection * Time.deltaTime, Space.Self);
    }
}
