
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Transition_Out : MonoBehaviour
{
    private Image img;


    [SerializeField] private float delay;
    [SerializeField] private float fillSpeed;

    public UnityEvent OnStart;
    public UnityEvent OnEnd;

    private void Awake()
    {
        img = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActivateOnStart", delay);
    }

    // Update is called once per frame
    void Update()
    {
        Fill();
    }

    private void Fill()
    {
        if (img.fillAmount >= 0.999f) // why this number, cause I like it
        {
            img.fillAmount = 1;
            OnEnd?.Invoke();
        }
        else
        {
            img.fillAmount += (1 - img.fillAmount) * fillSpeed * Time.deltaTime;
        }
    }
    private void ActivateOnStart()
    {
        OnStart?.Invoke();
    }
}
