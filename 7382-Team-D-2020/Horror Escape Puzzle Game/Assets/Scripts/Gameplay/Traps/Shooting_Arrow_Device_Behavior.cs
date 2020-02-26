using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("delay: " + delay + " repeat: " + repeatRate + " mode: " + firingMode + " Homing:" + projectile.GetComponent<Arrow_Behavior>().Homing);
    }

    private void ModeSelection()
    {
        Debug.Log("Update");
        switch (firingMode)
        {
            case FiringMode.Auto: // Automatic
                Debug.Log("Automatic") ;
                InvokeRepeating("Fire", delay, repeatRate);
                break;

            case FiringMode.Trigger: // Trigger
            case FiringMode.TriggerHoming:
                Debug.Log("trigger");
                this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                break;

            case FiringMode.Burst: // Burst
                //shoot 3 arrows in succession
                Debug.Log("burst");
                InvokeRepeating("Burst", delay, repeatRate);
                break;

            case FiringMode.Rand: //Random
                Debug.Log("rng");
                InvokeRepeating("Fire", Random.Range(0f, 5f), Random.Range(0f, 5f));
                break;

            case FiringMode.Homing: // Homing //Can be deleted if finish with debug
                Debug.Log("hoomin");
                goto case FiringMode.Auto;
        }
    }

    private void Burst()
    {
        InvokeRepeating("Fire", 0f, 1f);
    }

    private void Fire()
    {
        projectile.GetComponent<Arrow_Behavior>().Speed = (firingMode != FiringMode.Trigger)? this.arrowSpeed: this.arrowSpeed * 2f;
        projectile.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10f;
        if(firingMode == FiringMode.Homing) projectile.GetComponent<Arrow_Behavior>().Homing = true;
        Instantiate(projectile, this.gameObject.transform);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) Debug.Log("fire trigger"); Fire();

    }

}
