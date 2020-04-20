
using UnityEngine;

public class ColorblindSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] colorBlindPanel = null;

    //private void Awake()
    //{
    //    SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    //}

    //private void SceneManager_sceneUnloaded(Scene sc)
    //{
    //    if(sc.name == "MainMenuScene")
    //    {
    //        for (int i = 0; i < colorBlindPanel.Length; i++)
    //        {
    //            if (colorBlindPanel[i].activeSelf != true) Destroy(colorBlindPanel[i]);
    //        }
    //    }
    //}
    
    public void ChooseColor(int value)
    {
        Settings.Instance.ColorblindModePicker(value);
    }
 
}
