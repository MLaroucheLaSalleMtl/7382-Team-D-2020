
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private float delay;
    [SerializeField] private Animator anim;

    private float curTime = 0;
    private LoadSceneName script;
    [SerializeField] private AudioSource audioS;

    private void Awake()
    {
        script = GetComponent<LoadSceneName>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Play", delay);
    }
    private void Update()
    {
        curTime += Time.deltaTime;

        if (curTime - 2f >= audioS.clip.length)
        {
            script.LoadSceneDirectly("MainMenuScene");
        }
    }
    private void Play()
    {
        ps.Play();
        anim.enabled = true;
    }
}
