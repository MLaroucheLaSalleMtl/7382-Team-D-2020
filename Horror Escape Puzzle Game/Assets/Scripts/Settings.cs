
using System;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance = null;

    public static float DeathWaitTimer = 3f;

    public enum ColorblindMode { None, Blue, Yellow, Green };
    private static ColorblindMode colorblindMode = ColorblindMode.None;

    [SerializeField] private GameObject[] colorblindFilters = null;

    private void Awake()
    {
        CreateSingleton();

        Screen.SetResolution(1280, 720, false);

        LoadSavedSettings();
    }

    public void ColorblindModePicker(int value)
    {
        if (value == 0)
        {
            foreach (GameObject obj in colorblindFilters)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < colorblindFilters.Length; i++)
            {
                if (i == value - 1) 
                    colorblindFilters[i].SetActive(true);
                else
                    colorblindFilters[i].SetActive(false);
            }
        }
    }

    private void LoadSavedSettings()
    {
        colorblindMode = (ColorblindMode)PlayerPrefs.GetInt(nameof(colorblindMode), 0);
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
#if UNITY_EDITOR
            DestroyImmediate(gameObject);
#else
            Destroy(gameObject);
#endif
        }
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        Instance = null;

        SaveSettings();
    }
#endif

    private void OnApplicationQuit()
    {
        Instance = null;

        SaveSettings();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt(nameof(colorblindMode), (int)colorblindMode);
    }
}
