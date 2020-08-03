using UnityEngine;
using UnityEngine.Events;

public enum Player_SizeState
{
    Large,
    Medium,
    Small
}

public class Player_SizeController : MonoBehaviour
{
    //--- Public Variables ---//
    public AnimationCurve m_sizeCurve;
    public float m_medThreshold;
    public float m_smallThreshold;
    public float m_sizeChangeMaxDuration;



    //--- Private Variables ---//
    private UnityEvent<float> m_onSizeUpdated;
    private Player_SizeState m_currentSizeState;
    private Player_Respawner m_respawner;
    private float m_sizeChangeTimeSoFar;
    private float m_percentOfMaxSize;
    private float m_maxSize;
    private float m_currentSize;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_currentSizeState = Player_SizeState.Large;
        m_respawner = GetComponent<Player_Respawner>();
        m_sizeChangeTimeSoFar = 0.0f;
        PercentOfMaxSize = 1.0f;
        m_maxSize = m_sizeCurve.Evaluate(0.0f);
        m_currentSize = m_maxSize;
    }

    private void Update()
    {
        // Shrink over time
        ReduceSizeOverTime();

#if UNITY_EDITOR
        // Editor only: use the 1 - 3 keys to swap size stages at will
        if (Input.GetKeyDown(KeyCode.Alpha1))
            JumpToSizeStage(Player_SizeState.Large);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            JumpToSizeStage(Player_SizeState.Medium);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            JumpToSizeStage(Player_SizeState.Small);

        // Editor only: press space to drop to the next stage
        if (Input.GetKeyDown(KeyCode.Space))
            DropToNextSizeStage();
#endif
    }



    //--- Methods ---//
    public void ResetSize()
    {
        // Reset the scaling
        m_sizeChangeTimeSoFar = 0.0f;
        transform.localScale = Vector3.one * m_maxSize;
        m_currentSize = m_maxSize;
        PercentOfMaxSize = 1.0f;
    }

    public void ReduceSizeOverTime()
    {
        // Increase the timer
        m_sizeChangeTimeSoFar += Time.deltaTime;

        // If the time is up, we should respawn
        // Otherwise, we should adjust the scale instead
        if (m_sizeChangeTimeSoFar >= m_sizeChangeMaxDuration)
        {
            //m_respawner.Respawn();
        }
        else
        {
            // Calculate the new size
            float sizeChangeT = m_sizeChangeTimeSoFar / m_sizeChangeMaxDuration;
            m_currentSize = m_sizeCurve.Evaluate(sizeChangeT);

            // Update the size state
            if (sizeChangeT <= m_medThreshold)
                m_currentSizeState = Player_SizeState.Large;
            else if (sizeChangeT <= m_smallThreshold)
                m_currentSizeState = Player_SizeState.Medium;
            else
                m_currentSizeState = Player_SizeState.Small;

            // Apply the size to the transform
            transform.localScale = Vector3.one * m_currentSize;

            // Recalculate the percentage of the maximum size
            PercentOfMaxSize = 1.0f - sizeChangeT;
        }
    }

    public void DropToNextSizeStage()
    {
        // Jump large -> medium, medium -> small
        // Call this from the heatpad puzzle item
        if (m_currentSizeState == Player_SizeState.Large)
            JumpToSizeStage(Player_SizeState.Medium);
        else if (m_currentSizeState == Player_SizeState.Medium)
            JumpToSizeStage(Player_SizeState.Small);
    }



    //--- Utility Functions ---//
    private void JumpToSizeStage(Player_SizeState _newState)
    {
        // Store the new value
        m_currentSizeState = _newState;

        // Update all of the parameters based on the new state
        switch(m_currentSizeState)
        {
            case Player_SizeState.Large:
                PercentOfMaxSize = 1.0f;
                m_currentSize = m_maxSize;
                m_sizeChangeTimeSoFar = 0.0f;
                break;

            case Player_SizeState.Medium:
                PercentOfMaxSize = 1.0f - m_medThreshold;
                m_currentSize = m_sizeCurve.Evaluate(m_medThreshold);
                m_sizeChangeTimeSoFar = m_sizeChangeMaxDuration * m_medThreshold;
                break;

            case Player_SizeState.Small:
            default:
                PercentOfMaxSize = 1.0f - m_smallThreshold;
                m_currentSize = m_sizeCurve.Evaluate(m_smallThreshold);
                m_sizeChangeTimeSoFar = m_sizeChangeMaxDuration * m_smallThreshold;
                break;
        }

        // Apply the size to the transform
        transform.localScale = Vector3.one * m_currentSize;
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



    //--- Getters ---//
    public float GetCurrentRadius()
    {
        return m_currentSize;
    }

    public Player_SizeState GetCurrentSizeState()
    {
        return m_currentSizeState;
    }
}
