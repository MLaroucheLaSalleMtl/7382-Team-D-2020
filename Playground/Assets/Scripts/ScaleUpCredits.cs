
using UnityEngine;

public class ScaleUpCredits : MonoBehaviour
{

    void Start()
    {
        transform.localScale = new Vector3(0,0,0);

        InvokeRepeating(nameof(ScaleUp), 0f, 0.05f);
    }
    private void ScaleUp()
    {
        transform.localScale += Vector3.one * 5f * Time.deltaTime;

        if(transform.localScale.magnitude >= 2f) CancelInvoke();
    }
}
