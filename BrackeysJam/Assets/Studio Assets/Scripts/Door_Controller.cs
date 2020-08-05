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
    public float doorOpeningSpeed = 0.5f;
    public float doorClosingSpeed = 0.7f;

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
            lerpStep += doorOpeningSpeed * Time.deltaTime;
            newPosition = Vector3.Lerp(startPosition,desiredPosition, lerpStep);
            this.transform.position = newPosition;
        }
        else if(!controller.isActive && lerpStep > 0.0f)
        {

            lerpStep -= doorClosingSpeed * Time.deltaTime;
            newPosition = Vector3.Lerp(startPosition, desiredPosition, lerpStep);
            this.transform.position = newPosition;
        }
    }
}
