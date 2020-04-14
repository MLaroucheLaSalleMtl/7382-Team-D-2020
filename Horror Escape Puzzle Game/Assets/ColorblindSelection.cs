
using UnityEngine;

public class ColorblindSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] colorBlindPanel = null;

    public void ChooseColor(int value)
    {
        if(value == 0)
        {
            foreach (GameObject obj in colorBlindPanel)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < colorBlindPanel.Length; i++)
            {
                if (i == value - 1) colorBlindPanel[i].SetActive(true);
                else colorBlindPanel[i].SetActive(false);
            }
        }
    }
}
