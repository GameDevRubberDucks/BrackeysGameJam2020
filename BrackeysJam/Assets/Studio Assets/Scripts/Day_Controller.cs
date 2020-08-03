using UnityEngine;
using UnityEngine.Events;

public class Day_Controller : MonoBehaviour
{
    //--- Public Variables ---//
    public float m_dayLength;



    //--- Private Variables ---//
    private UnityEvent<float> m_onDayTimeUpdated;
    private UnityEvent m_onDayReset;
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

        // Can quickly reset the day by pressing the appropriate key
        if (Input.GetKeyDown(KeyCode.R))
            ResetDay();
    }



    //--- Methods ---//
    public void ResetDay()
    {
        // Reset all of the required systems
        m_dayTimeSoFar = 0.0f;
        OnDayTimeUpdated.Invoke(0.0f);
        m_respawner.Respawn();

        // Broadcast to everything in the scene that the day has been reset
        m_onDayReset.Invoke();
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

    public UnityEvent OnDayReset
    {
        get
        {
            if (m_onDayReset == null)
                m_onDayReset = new UnityEvent();

            return m_onDayReset;
        }
    }
}
