using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Behavior : MonoBehaviour
{

    private Camera cam;
    private AudioListener audioS;
    

    void Start()
    {
        cam = GetComponent<Camera>();
        audioS = GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FindandAttachOnPlayer()
    {
        //this.enabled = audioS.enabled = cam.enabled = true;
    }

}
