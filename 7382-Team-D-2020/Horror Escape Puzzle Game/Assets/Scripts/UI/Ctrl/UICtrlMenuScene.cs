using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICtrlMenuScene : MonoBehaviour
{
    /// <summary>
    /// MenuScene CreditBtnClickEvent
    /// </summary>
    public void OnCreditBtnClick()
    {
        //todo sceneloader
        SceneManager.LoadScene("CreditSetion");
    }
}
