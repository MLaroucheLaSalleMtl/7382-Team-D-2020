using System.Collections;
using UnityEngine;

public class Various : MonoBehaviour
{

    public void Quit()
    {
        Application.Quit();
    }

    public void Fade_B4_Action()
    {
        MusicManager.Instance.FadeOut();
        SceneLoaderManager.Instance.FadeOut();
        Controls.Instance.Locked = true;
    }
}
