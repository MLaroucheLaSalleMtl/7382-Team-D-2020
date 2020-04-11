
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Shooting_Arrow_Device_Behavior : MonoBehaviour
{

    #region Fields
    [SerializeField] private GameObject projectile;

    public enum FiringMode { None, Auto, Trigger, Burst, Rand, Homing, TriggerHoming}
    [HideInInspector, SerializeField] private FiringMode firingMode;
    
    //Invoke Repeating
    [HideInInspector, SerializeField] public float delay;
    [HideInInspector, SerializeField] public float repeatRate;

    [HideInInspector, SerializeField] public float burstDelay;

    [Range(1f,20f)]
    [SerializeField] private float arrowSpeed;


    //Tracking
    [SerializeField] private bool trackPlayer = false;

    public FiringMode PropFiringMode { get => firingMode; set => firingMode = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        projectile.GetComponent<Arrow_Behavior>().Homing = false;

        ModeSelection();
    }

    private void Update()
    {
        //if (trackPlayer) Track();
    }

    private void Track()
    {
        Transform playerPosition = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();

        if (playerPosition != null) gameObject.transform.LookAt(playerPosition,Vector3.forward);
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
                this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                break;

            case FiringMode.Burst:  // kind of useless
                //shoot 3 arrows in succession
                InvokeRepeating("Burst", delay, repeatRate);
                break;

            case FiringMode.Rand:
                StartCoroutine(RandomShots());
                break;

            case FiringMode.Homing:
                projectile.GetComponent<Arrow_Behavior>().Homing = true;
                goto case FiringMode.Auto;
          
        }
    }

    private void Burst()
    {
        StartCoroutine("EnumBurst");
    }

    private void Fire()
    {
        projectile.GetComponent<Arrow_Behavior>().Speed = (firingMode != FiringMode.Trigger) ? this.arrowSpeed : this.arrowSpeed * 2f;
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
