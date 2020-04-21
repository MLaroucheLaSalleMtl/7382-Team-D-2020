
using UnityEngine;
using UnityEngine.Playables;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps = null;
    [SerializeField] private float vfxDelay = 4f;
    [SerializeField] private float playableDelay = 1f; // taking in consideration the fact that scene activation is not synced
    [SerializeField] private PlayableDirector playable = null;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke(nameof(PlayVFX), vfxDelay);
        Invoke(nameof(PlayTimeline), playableDelay);
    }

    private void PlayVFX()
    {
        ps.Play();
    }

    private void PlayTimeline()
    {
        playable.Play();
    }
}
