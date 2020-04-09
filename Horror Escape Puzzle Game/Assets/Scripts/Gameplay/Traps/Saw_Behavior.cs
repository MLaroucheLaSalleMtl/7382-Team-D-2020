
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Saw_Behavior : MonoBehaviour
{

    #region UnityEditor
    private enum Behavior { Default, Translate, Orbit }
    [SerializeField] private Behavior behavior = Behavior.Default;
    private enum Rotation { Clockwise, Counter_Clockwise }
    [SerializeField] private Rotation rotate = Rotation.Clockwise;

    [SerializeField] private Transform[] waypoints;

    
    [Range(0f,360f), SerializeField] private float speed = 0f;

    [Tooltip("Do you want the Saw to follow the path forever?")]
    [HideInInspector] private int targetWaypoint = 0;
    [HideInInspector] private LineRenderer path;


    //For the saw orbit
    [Tooltip("Create an Anchor, then make Saw a child of Anchor.")]
    [SerializeField] private Transform anchor;
    #endregion

    private void Awake()
    {
        path = this.GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
        path.startColor = path.endColor = Color.white;
        path.startWidth = path.endWidth = 0.1f;
        path.useWorldSpace = true;


        switch (behavior)
        {
            case Behavior.Translate:
                this.gameObject.transform.position = anchor.position;
                anchor.position = waypoints[0].position;
                path.positionCount = waypoints.Length + 1;
                DrawPath(); 
                break;

            case Behavior.Orbit:
                path.positionCount = 2;
                break;
        }
    }

    private void DrawPath()
    {
        switch (behavior)
        {
            case Behavior.Translate:

                for (int i = 0; i < waypoints.Length; i++)
                {
                    path.SetPosition(i, waypoints[i].position);
                }
                path.SetPosition(path.positionCount - 1, waypoints[0].position);
                break;

            case Behavior.Orbit:
                path.SetPosition(0, transform.position);
                path.SetPosition(1, anchor.position);
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (behavior)
        {
            case Behavior.Translate:

                Vector2 direction = (waypoints[targetWaypoint].position - anchor.position).normalized; 
                Vector2 currentPos = anchor.position;
                Vector2 destination = waypoints[targetWaypoint].position;

                if (Vector2.Distance(currentPos, destination) >= 0.1f)
                {
                    anchor.Translate(direction * speed * Time.deltaTime);
                }
                else if (targetWaypoint + 1 < waypoints.Length) //prevent index out of range error
                {
                    //Previous waypoint reached. Going to the next one.
                    targetWaypoint++;
                }
                else if (targetWaypoint == waypoints.Length - 1)
                {
                    targetWaypoint = 0;
                }
                break;

            case Behavior.Orbit:

                switch (rotate)
                {
                    case Rotation.Clockwise:
                        anchor.Rotate(0, 0, -speed * Time.deltaTime);
                        break;
                    case Rotation.Counter_Clockwise:
                        anchor.Rotate(0, 0, speed * Time.deltaTime);
                        break;
                }
                DrawPath(); // needs to be updated
                break;

        }
    }
 

}

