using System.Collections;
using UnityEngine;

public class Various : MonoBehaviour
{

    public void Quit()
    {
        StartCoroutine(IEnumQuitApplication());
    }


    private IEnumerator IEnumQuitApplication()
    {
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
