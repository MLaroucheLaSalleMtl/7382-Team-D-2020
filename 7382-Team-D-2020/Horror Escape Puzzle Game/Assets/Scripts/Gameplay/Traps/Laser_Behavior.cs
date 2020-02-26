using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Behavior : MonoBehaviour
{

    [SerializeField] private GameObject[] assetsToTurnOff;

    public void Disable()
    {
        foreach(GameObject obj in assetsToTurnOff)
        {
            obj.SetActive(false);
        }
    }
}
