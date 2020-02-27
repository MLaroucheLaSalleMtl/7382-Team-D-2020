using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Death_Panel_Behavior : MonoBehaviour
{
    [SerializeField] private Text deathTxt;
    [SerializeField] private float timeBeforeClosing;
    // Start is called before the first frame update


    private void OnEnable()
    {
        deathTxt.text = "" + CurrentSessionPlayerData.Life;
        Invoke("ClosePanel", timeBeforeClosing);

    }

    private void ClosePanel()
    {
        Debug.Log("Closing");
        this.gameObject.SetActive(false);
    }
}
