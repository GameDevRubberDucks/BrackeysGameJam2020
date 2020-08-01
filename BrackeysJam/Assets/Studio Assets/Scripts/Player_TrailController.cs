using UnityEngine;
using System.Collections.Generic;

public class Player_TrailController : MonoBehaviour
{
    //--- Public Variables ---//
    public LineRenderer m_line;
    public float m_heightBias;
    public float m_posAddTimer;



    //--- Private Variables ---//
    private Player_SizeController m_sizeController;
    private float m_timeSinceLastPosAdd;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_sizeController = GetComponent<Player_SizeController>();
        m_line.widthCurve = new AnimationCurve();
        m_timeSinceLastPosAdd = 0.0f;
    }

    private void Start()
    {
        // Add the first point to the line
        AddPointToLine();
    }

    private void Update()
    {
        // If enough time has passed, add a new point to the line
        m_timeSinceLastPosAdd += Time.deltaTime;
        if (m_timeSinceLastPosAdd >= m_posAddTimer)
            AddPointToLine();
    }



    //--- Methods ---//
    public void ResetLine()
    {
        // Clear all of the positions in the line
        m_line.positionCount = 0;
        m_line.widthCurve = new AnimationCurve();
        m_timeSinceLastPosAdd = 0.0f;
        AddPointToLine();
    }

    public void AddPointToLine()
    {
        // Add the new position
        m_line.positionCount++;
        Vector3 newPos = transform.position;
        newPos.y -= m_sizeController.GetCurrentSize() / 2.0f;
        newPos.y += m_heightBias;
        m_line.SetPosition(m_line.positionCount - 1, newPos);

        // Update the width to match the size of the player
        float playerSize = m_sizeController.GetCurrentSize();
        float lifetimePercentage = 1.0f - m_sizeController.PercentOfMaxSize;
        var newWidthCurve = m_line.widthCurve;
        newWidthCurve.AddKey(lifetimePercentage, playerSize);
        m_line.widthCurve = newWidthCurve;

        // Reset the timer
        m_timeSinceLastPosAdd = 0.0f;
    }
}
