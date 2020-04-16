

using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
public class Platform_Behavior : MonoBehaviour
{
    
    [Tooltip("2 waypoints are enough")]
    [SerializeField] private Transform[] waypoints = null;
    [SerializeField] private float speed = 1f;

    //Custom Update Values
    [SerializeField] private float initDelay = 0f;

    [Tooltip("Default value = 0.0001f; Do not change default value unless special need.")] // because it's 1ms
    [SerializeField] private float repeateRate = 0.0001f;
    [SerializeField] private float platformIdleTime = 0f;
    private bool toggle = false;

    private int targetWaypoint = 1;

    void Start()
    {
        ToggleOn();

        this.gameObject.transform.position = waypoints[0].position;

        InvokeRepeating("CustomUpdate", initDelay, repeateRate);
    }

    //TODO: Prob make it to have more waypoints? and instead of going to origin, go back to previous waypoint?
    private void CustomUpdate()
    {
        if (toggle)
        {
            if (!HasArrived)
            {
                MovePlatform();
            }
            else
            {
                targetWaypoint++;
                ToggleOff();
                if (targetWaypoint == waypoints.Length) targetWaypoint = 0;
                Invoke("ToggleOn", platformIdleTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) collision.gameObject.transform.parent = this.gameObject.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) this.gameObject.transform.DetachChildren();
    }

    private void ToggleOn() => toggle = true; 

    private void ToggleOff() => toggle = false; 

    private void MovePlatform()
    {
        Vector2 direction = (waypoints[targetWaypoint].position - this.gameObject.transform.position).normalized;
        gameObject.transform.Translate(direction * speed * Time.deltaTime);
    }

    private bool HasArrived => (this.gameObject.transform.position - waypoints[targetWaypoint].position).magnitude <= 0.1;


}
