
using UnityEngine;
using UnityEngine.UI;
public class Death_Panel_Behavior : MonoBehaviour
{
    [SerializeField] private Text deathTxt;
    [SerializeField] private float timeBeforeClosing;


    private void OnEnable()
    {
        deathTxt.text = "" + CurrentSessionPlayerData.Life;
        Invoke("ClosePanel", timeBeforeClosing);

    }

    private void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }


}
