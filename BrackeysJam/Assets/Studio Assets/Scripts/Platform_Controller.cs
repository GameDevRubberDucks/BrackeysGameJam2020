using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Controller : MonoBehaviour
{

    //--- Setup Variable ---//


    //--- Public Variables ---//
    public Vector3[] pointPositions; //List of all the points' positions
    public float tolerance; //How close does the platform get to a point before snapping
    public float speed; //How fast the platform moves
    public float delayAmount; // The amount of time a platform stays at a point for
    public bool endOnPoint = false; //Used for running endOnPoint function
    //public bool inverse = false;
    //public bool vertical = false;

    //--- Private Variables ---//
    private Vector3 currentTarget;
    [SerializeField] private int numPoint = 0;
    private float delayTimer;
    private bool reversePositionsDirection = false;


    // Start is called before the first frame update
    void Start()
    {
        if (pointPositions.Length > 0)
        {
            currentTarget = pointPositions[0];
        }
        tolerance = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Version 2
        if (transform.parent.position != currentTarget)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }
    }

    private void MovePlatform()
    {
        Vector3 heading = currentTarget - transform.parent.position;
        transform.parent.position += (heading / heading.magnitude) * speed * Time.deltaTime;

        if (heading.magnitude < tolerance)
        {
            transform.parent.position = currentTarget;
            delayTimer = Time.time;
        }
    }
    private void UpdateTarget()
    {
        if (Time.time - delayTimer > delayAmount)
        {
            NextPlatform();
        }
    }

    private void NextPlatform()
    {
        if (reversePositionsDirection)
        {
            numPoint--;
            if (numPoint < 0)  //Ensures index does not go out of array bounds
                numPoint = 0;
        }
        else
            numPoint++;
        if (endOnPoint)
        {
            numPoint = pointPositions.Length - 1;
        }
        else if (numPoint >= pointPositions.Length && !endOnPoint) //NumPoint will ping pong from 0 to max and back to 0
        {
            //numPoint = 0;
            reversePositionsDirection = true;
            numPoint = pointPositions.Length - 1; //Ensures index does not go out of array bounds
        }
        else if (numPoint <= 0 && !endOnPoint)
        {
            reversePositionsDirection = false;
        }


        currentTarget = pointPositions[numPoint];
    }

    public void EndOnPoint()
    {
        endOnPoint = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(this.transform.parent);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

}
