using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UpdateVolText : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM;

    private Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
    }

    public void UpdateText(string paraName)
    {
        audioM.GetFloat(paraName, out float num);

        num = (1 - (Mathf.Abs(num) / 80) )* 100;

        txt.text = num.ToString() + "%";
    }
}
