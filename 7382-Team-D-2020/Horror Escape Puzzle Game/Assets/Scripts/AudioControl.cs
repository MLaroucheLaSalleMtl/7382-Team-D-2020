
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM;
    [SerializeField] private Slider sVolMaster, sVolUI, sVolSFX, sVolMusic;

    // Start is called before the first frame update
    void Start()
    {

        audioM.GetFloat("volMaster", out float fM);
        audioM.GetFloat("volMusic", out float fMusic);
        audioM.GetFloat("volUI", out float fUI);
        audioM.GetFloat("volSFX", out float fSFX);

        sVolMaster.value = fM;
        sVolUI.value = fUI;
        sVolSFX.value = fSFX;
        sVolMusic.value = fMusic;

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

}
