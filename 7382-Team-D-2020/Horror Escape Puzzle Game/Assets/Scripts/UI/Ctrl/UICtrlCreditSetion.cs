using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICtrlCreditSetion : MonoBehaviour
{
    /// <summary>
    /// MenuScene CreditBtnClickEvent
    /// </summary>
    public void OnBackClick()
    {
        //todo sceneloader
        SceneManager.LoadScene("MainMenuScene");
    }
}
