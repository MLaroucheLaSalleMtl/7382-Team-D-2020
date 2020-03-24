using UnityEngine;
using UnityEngine.Audio;


public class AudiomixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM;
    [SerializeField] private int volSteps = 5;

    public void IncreaseVol(string paraName)
    {
        float currVol=0;

        audioM.GetFloat(paraName, out currVol);

        currVol = currVol + volSteps > 0 ? 0: currVol + volSteps ;

        audioM.SetFloat(paraName, currVol);
    }

    public void DecreaseVol(string paraName)
    {
        float currVol = 0 ;

        audioM.GetFloat(paraName, out currVol);

        currVol = currVol - volSteps < -80 ? -80 : currVol - volSteps;

        audioM.SetFloat(paraName, currVol);
    }

}