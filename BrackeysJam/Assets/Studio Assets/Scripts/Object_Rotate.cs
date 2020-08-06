using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Rotate : MonoBehaviour
{
    public Vector3 endRot;

    private bool startRotation = false;
    private float lerp;

    private Vector3 startRot;


    // Start is called before the first frame update
    void Start()
    {
        startRot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (startRotation)
        {
            lerp += 1 * Time.deltaTime;
            transform.rotation = Quaternion.Euler(Vector3.Lerp(startRot, endRot, lerp));
        }
    }

    public void StartRotate()
    {
        startRotation = true;
        lerp = 0.0f;
    }
}
