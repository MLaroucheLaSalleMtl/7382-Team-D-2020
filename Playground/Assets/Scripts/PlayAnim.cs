using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
public void Play()
    {
        GetComponent<Animation>().Play();
    }
}
