using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox_Communicator: MonoBehaviour
{

    //--- Setup Variable ---//
    private PowerBox_Controller[] powerBoxs;
    //--- Public Variables ---//
    public bool isPairing = false;
    public bool isActive =false;

    
    
    // Start is called before the first frame update
    void Start()
    {
        powerBoxs = GetComponentsInChildren<PowerBox_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        int activeCount = ActiveCheck();

        if (activeCount == powerBoxs.Length)
        {
            isActive = true;
            isPairing = false;
        }
        else if (activeCount > 0 )
        {
            isActive = false;
            isPairing = true;
        }
        else
        {
            isActive = false;
            isPairing = false;
        }
    }


    private int ActiveCheck()
    {
        int activeCounter = 0;
        for (int x = 0; x < powerBoxs.Length; x++)
        {
            if (powerBoxs[x].m_isActive)
            {
                activeCounter++;
            }
        }
        return activeCounter;
    }
}
