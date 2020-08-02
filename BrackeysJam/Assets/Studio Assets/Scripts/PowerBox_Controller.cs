using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox_Controller : MonoBehaviour
{

    //--- Setup Variables ---//
    public GameObject[] balls;
    //--- Public Variables ---//
    public bool m_isActive = false;
    public bool m_isPairing = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentInParent<PowerBox_Communicator>().isPairing && m_isActive)
        {
            balls[0].GetComponent<Renderer>().material.SetColor("_BaseColor",Color.blue);
            balls[1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.blue);
        }
        else if (GetComponentInParent<PowerBox_Communicator>().isActive)
        {
            balls[0].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
            balls[1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
        }
        else
        {
            balls[0].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
            balls[1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(m_isActive == false)
            {
                m_isActive = true;
            }
        }
    }
}
