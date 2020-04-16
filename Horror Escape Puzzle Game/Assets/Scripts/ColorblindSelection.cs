
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorblindSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] colorBlindPanel = null;

    private void Awake()
    {
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene sc)
    {
        if(sc.name == "MainMenuScene")
        {
            for (int i = 0; i < colorBlindPanel.Length; i++)
            {
                if (colorBlindPanel[i].activeSelf != true) Destroy(colorBlindPanel[i]);
            }
        }
    }

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
