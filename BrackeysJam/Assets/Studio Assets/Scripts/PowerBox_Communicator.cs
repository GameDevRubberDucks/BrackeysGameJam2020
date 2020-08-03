using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerBox_Communicator: MonoBehaviour
{

    //--- Setup Variable ---//
    private PowerBox_Controller[] powerBoxs;
    //--- Public Variables ---//
    public bool isPairing = false;
    public bool isActive = false;
    public UnityEvent onPowerBoxesConnected;
    //--- Private Variables ----//
    private bool canFireEvent = false;

    
    
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

            if (canFireEvent)
            {
                onPowerBoxesConnected.Invoke();
                canFireEvent = false;
            }
        }
        else if (activeCount > 0 )
        {
            isActive = false;
            isPairing = true;
            canFireEvent = true;
        }
        else
        {
            isActive = false;
            isPairing = false;
            canFireEvent = true;
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
