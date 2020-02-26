using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Saw_Behavior : MonoBehaviour
{

    #region UnityEditor
    [SerializeField] private enum Behavior { Default, Translate, Orbit }
    [SerializeField] private Behavior behavior;
    [SerializeField] private Transform[] waypoints;
    [Range(0f,360f)][SerializeField] private float speed;
    [Tooltip("Do you want the Saw to follow the path forever?")]
    [HideInInspector] private int targetWaypoint = 0;
    [HideInInspector] private LineRenderer path;


    //For the saw orbit
    [SerializeField] private Transform anchor;
    #endregion



    // Start is called before the first frame update
    private void Start()
    {
        path = this.GetComponent<LineRenderer>();

        path.startColor = path.endColor = Color.white;
        path.startWidth = path.endWidth = 0.1f;
        path.useWorldSpace = true;


        switch (behavior)
        {
            case Behavior.Translate:

                path.positionCount = waypoints.Length + 1;
                DrawPath(); // Drawing it here cause only to be drawn once
                break;


            case Behavior.Orbit:

                path.positionCount = 2;
                break;
        }
       // Debug.Log(behavior.ToString());

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

                path.SetPosition(0, this.gameObject.transform.position);
                path.SetPosition(1, anchor.position);
                break;
        }
    }

    private void Update()
    {
        switch (behavior)
        {
            case Behavior.Translate:

                Vector2 direction = (waypoints[targetWaypoint].position - this.gameObject.transform.position).normalized; 
                Vector2 currentPos = this.transform.position;
                Vector2 destination = waypoints[targetWaypoint].position;

                if (Vector2.Distance(currentPos, destination) >= 0.1f)
                {
                    this.gameObject.transform.Translate(direction * speed * Time.deltaTime);
                    //Debug.Log(Vector2.Distance(currentPos, destination));
                }
                else if (targetWaypoint + 1 < waypoints.Length) //prevent index out of range error
                {
                    //Previous waypoint reached. Going to the next one.
                    targetWaypoint++;
                    Debug.Log("Waypoint: " + targetWaypoint);
                }
                else if (targetWaypoint == waypoints.Length - 1)
                {
                    targetWaypoint = 0;
                    Debug.Log("Waypoint: " + targetWaypoint);
                }
                Debug.DrawRay(currentPos, waypoints[targetWaypoint].position - this.gameObject.transform.position);

                break;


            case Behavior.Orbit:
                
                anchor.Rotate(0, 0, speed * Time.deltaTime);
                DrawPath(); // needs to be updated
                break;

        }
    }
 

}

