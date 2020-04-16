
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM = null;
    [SerializeField] private Slider sVolMaster = null, 
                                    sVolUI = null, 
                                    sVolSFX = null, 
                                    sVolMusic = null;

    // Start is called before the first frame update
    void Start()
    {

        audioM.GetFloat("volMaster", out float fM);
        audioM.GetFloat("volMusic", out float fMusic);
        audioM.GetFloat("volUI", out float fUI);
        audioM.GetFloat("volSFX", out float fSFX);

        sVolMaster.value = ConvertDecibelToPercentage(ref fM);
        sVolUI.value = ConvertDecibelToPercentage(ref fUI);
        sVolSFX.value = ConvertDecibelToPercentage(ref fSFX);
        sVolMusic.value = ConvertDecibelToPercentage(ref fMusic);

    }

    private float ConvertDecibelToPercentage(ref float value) => ((value + 40f) / 40f) * 100;
    private float ConvertPercentageToDecibel(ref float value) 
    {
        float temp = (1 - (value * 0.01f)) * -40f;
        return temp <= -40f ? -80f : temp;
    }

    public void ControlMaster(float sliderValue)
    {
        audioM.SetFloat("volMaster", ConvertPercentageToDecibel(ref sliderValue));
    }

    public void ControlSFX(float sliderValue)
    {
        audioM.SetFloat("volSFX", ConvertPercentageToDecibel(ref sliderValue));
    }

    public void ControlMusic(float sliderValue)
    {
        audioM.SetFloat("volMusic", ConvertPercentageToDecibel(ref sliderValue));
    }

    public void ControlUI(float sliderValue)
    {
        audioM.SetFloat("volUI", ConvertPercentageToDecibel(ref sliderValue));
    }

}
