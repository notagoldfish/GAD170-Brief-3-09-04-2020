using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Using Video https://www.youtube.com/watch?v=9KdY4mafG_E

public class Elevator : MonoBehaviour
{
    public Vector3[] points; //Creates customisable array for the destinations for the platform, we will only use two for our elevator
    public int pointNumber = 0; //Represents which point we are currently looking at (0 is the first point, ie starting point)
    private Vector3 currentTarget; //The current target to get to
    public float tolerance; //Help the platform snap cleanly to its destination point
    public float speed; //speed
    public float delayTime; //Time spent at its point before moving on
    private float delayStart; //Gives a start for our delay time so it doesn't delay on start
    public bool automatic; //Bool for whether or not its a triggerable event or whether it will just continue forever

    private void Start()
    {
        //This just snaps our platform to its starting point
        if (points.Length > 0)
        {
            currentTarget = points[0];
        }
        //This sets the tolerance so its at the right amount to snap smoothly to its destination
        tolerance = speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //If you aren't at the position you're suppose to be at then move to it, if you are at your target then update to a new target
        if (transform.position != currentTarget)
        {
            MovePlatform();  //Initialises the MovePlatform
        } else
        {
            UpdateTarget(); //Initialises the UpdateTarget
        }
    }

    
    void MovePlatform()
    {
        Vector3 heading = currentTarget - transform.position; //Figures out what direction the platform is going
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime; //Will use the heading divided by the heading's magnitude to find a normalised vector and will use the speed to head their
        if (heading.magnitude < tolerance) //Once the platform reaches the tolerance zone it will snap it into its destination. Then this will start the delay
        {
            transform.position = currentTarget;
            delayStart = Time.time;
        }
    }

    void UpdateTarget()
    {
        if (automatic) //checks if automatic is true
        {
            if (Time.time - delayStart > delayTime) //checks if it is ready to start
            {
                NextPlatform(); //Initialises NextPlatform
            }
        }
    }
    public void NextPlatform()
    {
        pointNumber++; //Will set the next destination for the next point in the array
        if (pointNumber >= points.Length) //Sets it that when the points reach the end of the array it will start over at 0
        {
            pointNumber = 0;
        }
        currentTarget = points[pointNumber]; //sets the current target as the point number
    }

    //Just sets whatever enters its trigger collider to be the child of it this object so that it follows its movement. Unfortunately due to some scaling problems this means we can't put the elevators
    //in the main layout organisation I've done so they just have to be seperate or else objects that enter its trigger sometimes skew
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
