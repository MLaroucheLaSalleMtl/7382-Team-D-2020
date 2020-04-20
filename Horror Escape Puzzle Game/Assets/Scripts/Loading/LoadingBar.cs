
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingBar : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (GetComponent<Image>().fillAmount >= 1f)
            return;

        GetComponent<Image>().fillAmount = SceneLoaderManager.Instance.Progress;
    }
}
