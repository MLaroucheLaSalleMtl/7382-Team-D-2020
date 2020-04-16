
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Shooting_Arrow_Device_Behavior : MonoBehaviour
{

    #region Fields
    [SerializeField] private GameObject projectile = null;

    public enum FiringMode { None, Auto, Trigger, Burst, Rand, Homing, TriggerHoming}
    [HideInInspector, SerializeField] private FiringMode firingMode = FiringMode.None;
    
    //Invoke Repeating
    [HideInInspector, SerializeField] public float delay = 0f;
    [HideInInspector, SerializeField] public float repeatRate = 1f;
    [HideInInspector, SerializeField] public float burstDelay = 0f;

    [Range(1f,20f)]
    [SerializeField] private float arrowSpeed = 0f;

    //Tracking
    [SerializeField] private bool trackPlayer = false;

    public FiringMode PropFiringMode { get => firingMode; set => firingMode = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject) gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if(projectile) projectile.GetComponent<Arrow_Behavior>().Homing = false;

        ModeSelection();
    }

    private void Update()
    {
        if (trackPlayer) Track();
    }

    private void Track()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            Transform trans = target.GetComponent<Transform>();

            transform.up = trans.position - transform.position;
        }
    }

    private void ModeSelection()
    {
        switch (firingMode)
        {
            case FiringMode.Auto:
                InvokeRepeating("Fire", delay, repeatRate);
                break;

            case FiringMode.Trigger:
            case FiringMode.TriggerHoming:
                if(gameObject) gameObject.GetComponent<BoxCollider2D>().enabled = true;
                break;

            case FiringMode.Burst:  // kind of useless
                //shoot 3 arrows in succession
                InvokeRepeating("Burst", delay, repeatRate);
                break;

            case FiringMode.Rand:
                StartCoroutine(RandomShots());
                break;

            case FiringMode.Homing:
                if(projectile) projectile.GetComponent<Arrow_Behavior>().Homing = true;
                goto case FiringMode.Auto;
          
        }
    }

    private void Burst()
    {
        StartCoroutine("EnumBurst");
    }

    private void Fire()
    {
        if(projectile) projectile.GetComponent<Arrow_Behavior>().Speed = (firingMode != FiringMode.Trigger) ? this.arrowSpeed : this.arrowSpeed * 2f;
        Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) Fire();
    }

    private IEnumerator RandomShots()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 10f));
            Fire();
        }
    }

    private IEnumerator EnumBurst() // Coroutine
    {
        for(int i = 0; i < 3; i++) // Burst of 3 shots
        {
            yield return new WaitForSeconds(burstDelay);
            Fire();
        }
    }
}
