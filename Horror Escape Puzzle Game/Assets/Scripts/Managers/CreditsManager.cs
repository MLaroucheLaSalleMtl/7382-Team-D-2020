
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private float delay;
    [SerializeField] private GameObject fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        fadeOut.SetActive(true);
        Invoke("Play", delay);
    }

    private void Play()
    {
        ps.Play();
    }

}
