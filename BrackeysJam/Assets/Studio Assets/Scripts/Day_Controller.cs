using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class Day_Controller : MonoBehaviour
{
    //--- Public Variables ---//
    public float m_dayLength;



    //--- Private Variables ---//
    private UnityEvent<float> m_onDayTimeUpdated;
    private Player_Respawner m_respawner;
    private float m_dayTimeSoFar;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_respawner = FindObjectOfType<Player_Respawner>();
        m_dayTimeSoFar = 0.0f;
        OnDayTimeUpdated.Invoke(0.0f);
    }

    private void Update()
    {
        // Increase the timer
        m_dayTimeSoFar += Time.deltaTime;

        // If the end of the day has been reached, reset the day
        // Otherwise, progress the day visualization
        if (m_dayTimeSoFar >= m_dayLength)
        {
            ResetDay();
        }
        else
        {
            // Send the event with the percentage of the day completed
            OnDayTimeUpdated.Invoke(m_dayTimeSoFar / m_dayLength);
        }
    }



    //--- Methods ---//
    public void ResetDay()
    {
        // Reset all of the required systems
        m_dayTimeSoFar = 0.0f;
        OnDayTimeUpdated.Invoke(0.0f);
        m_respawner.Respawn();
        ResetInteractables();


    }
    public void ResetInteractables()
    {
        Item_behavior[] items = GameObject.FindObjectsOfType<Item_behavior>();
        KeyUnlockPad[] unlockPads = GameObject.FindObjectsOfType<KeyUnlockPad>();
        Player_ItemInteractions playerInteraction = GameObject.FindObjectOfType<Player_ItemInteractions>();

        //reset each interactable
        foreach (Item_behavior item in items)
        {
            item.ResetObject();
        }
        foreach(KeyUnlockPad unlockPad in unlockPads)
        {
            unlockPad.ObjectReset();
        }
        playerInteraction.ResetObject();

    }



    //--- Properties ---//
    public UnityEvent<float> OnDayTimeUpdated
    {
        get
        {
            if (m_onDayTimeUpdated == null)
                m_onDayTimeUpdated = new UnityEvent<float>();

            return m_onDayTimeUpdated;
        }
    }
}
