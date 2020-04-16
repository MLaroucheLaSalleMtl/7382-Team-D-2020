
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps = null;
    [SerializeField] private float delay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Play", delay);
    }

    private void Play()
    {
        ps.Play();
    }

}
