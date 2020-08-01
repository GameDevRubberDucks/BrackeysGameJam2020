using UnityEngine;
using UnityEngine.Events;

public class Player_SizeController : MonoBehaviour
{
    //--- Public Variables ---//
    public float m_sizeChangeMaxDuration;
    public float m_minSize;



    //--- Private Variables ---//
    private UnityEvent<float> m_onSizeUpdated;
    private Player_Respawner m_respawner;
    private float m_maxSize;
    private float m_sizeChangeTimeSoFar;
    private float m_percentOfMaxSize;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_respawner = GetComponent<Player_Respawner>();
        m_maxSize = transform.localScale.x;
        m_sizeChangeTimeSoFar = 0.0f;
        PercentOfMaxSize = 1.0f;
    }



    //--- Methods ---//
    public void ResetSize()
    {
        // Reset the scaling
        m_sizeChangeTimeSoFar = 0.0f;
        transform.localScale = Vector3.one * m_maxSize;
    }

    public void ReduceSize(float _timeIncrement)
    {
        // Increase the timer
        // The time increment param means we can reduce size via Time.fixedDeltaTime if needed (movement is physics based)
        // Otherwise, it should just be Time.deltaTime
        m_sizeChangeTimeSoFar += _timeIncrement;

        // If the time is up, we should respawn
        // Otherwise, we should adjust the scale instead
        if (m_sizeChangeTimeSoFar >= m_sizeChangeMaxDuration)
        {
            m_respawner.Respawn();
        }
        else
        {
            // Calculate the new size
            float sizeChangeT = m_sizeChangeTimeSoFar / m_sizeChangeMaxDuration;
            float newSize = Mathf.Lerp(m_maxSize, m_minSize, sizeChangeT);

            // Apply the size to the transform
            transform.localScale = Vector3.one * newSize;

            // Recalculate the percentage of the maximum size
            PercentOfMaxSize = 1.0f - sizeChangeT;
        }
    }



    //--- Properties ---//
    public UnityEvent<float> OnSizeUpdated 
    {
        get
        {
            if (m_onSizeUpdated == null)
                m_onSizeUpdated = new UnityEvent<float>();

            return m_onSizeUpdated;
        }
    }

    public float PercentOfMaxSize
    {
        get { return m_percentOfMaxSize; }
        set
        {
            m_percentOfMaxSize = value;
            OnSizeUpdated.Invoke(m_percentOfMaxSize);
        }
    }
}
