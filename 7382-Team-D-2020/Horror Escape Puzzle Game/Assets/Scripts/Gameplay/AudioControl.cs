using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ControlMaster(float sliderValue)
    {
        audioM.SetFloat("volMaster", Mathf.Log10(sliderValue) * 20);
    }

    public void ControlSFX(float sliderValue)
    {
        audioM.SetFloat("volSFX", Mathf.Log10(sliderValue) * 20);
    }

    public void ControlMusic(float sliderValue)
    {
        audioM.SetFloat("volMusic", Mathf.Log10(sliderValue) * 20);
    }

    public void ControlUI(float sliderValue)
    {
        audioM.SetFloat("volUI", Mathf.Log10(sliderValue) * 20);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
