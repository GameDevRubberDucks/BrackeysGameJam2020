using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{

    //--- Setup Variable ---//
    public PowerBox_Communicator controller;
    //--- Public Variables ---//
    public bool isOpen = false;
    public Vector3 desiredPosition;

    //--- Private Variables ---//
    private Vector3 newPosition;
    private Vector3 startPosition;
    private float lerpStep = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isActive && lerpStep < 1.0f)
        {
            lerpStep += 0.5f * Time.deltaTime;
            newPosition = Vector3.Lerp(startPosition,desiredPosition, lerpStep);
            this.transform.position = newPosition;
        }
        else if(!controller.isActive && lerpStep > 0.0f)
        {

            lerpStep -= 0.5f * Time.deltaTime;
            newPosition = Vector3.Lerp(startPosition, desiredPosition, lerpStep);
            this.transform.position = newPosition;
        }
    }
}
