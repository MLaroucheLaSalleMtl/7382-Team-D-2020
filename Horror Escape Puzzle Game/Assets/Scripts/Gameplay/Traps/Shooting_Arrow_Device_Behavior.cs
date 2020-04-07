using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Shooting_Arrow_Device_Behavior : MonoBehaviour
{

    #region Fields
    [SerializeField] private GameObject projectile;

    public enum FiringMode { None, Auto, Trigger, Burst, Rand, Homing, TriggerHoming}
    [HideInInspector][SerializeField] private FiringMode firingMode;
    
    //Invoke Repeating
    [HideInInspector][SerializeField] public float delay;
    [HideInInspector][SerializeField] public float repeatRate;

    [HideInInspector][SerializeField] public float burstDelay;

    [Range(1f,20f)]
    [SerializeField] private float arrowSpeed;

    [HideInInspector] private Transform playerPosition;

    public FiringMode PropFiringMode { get => firingMode; set => firingMode = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        projectile.GetComponent<Arrow_Behavior>().Homing = false;

        ModeSelection();
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
                InvokeRepeating("Fire", Random.Range(0f, 5f), Random.Range(0f, 5f));
                break;

            case FiringMode.Homing: 
                goto case FiringMode.Auto;
        }
    }

    private void Burst()
    {
        StartCoroutine("EnumBurst");
    }

    private void Fire()
    {
        projectile.GetComponent<Arrow_Behavior>().Speed = (firingMode != FiringMode.Trigger)? this.arrowSpeed: this.arrowSpeed * 2f;

        if(firingMode == FiringMode.Homing) projectile.GetComponent<Arrow_Behavior>().Homing = true;

        Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) Fire();
    }

    private IEnumerator EnumBurst() // Coroutine
    {
        for(int i = 0; i < 3; i++) // Burst of 3 shots
        {
            Fire();
            yield return new WaitForSeconds(burstDelay);
        }
    }
}
