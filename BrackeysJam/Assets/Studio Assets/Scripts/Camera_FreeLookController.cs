using UnityEngine;
using Cinemachine;

public class Camera_FreeLookController : MonoBehaviour
{
    //--- Private Variables ---//
    private CinemachineFreeLook m_freeLookCam;
    private string m_xAxisName;
    private string m_yAxisName;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_freeLookCam = GetComponent<CinemachineFreeLook>();
        m_xAxisName = m_freeLookCam.m_XAxis.m_InputAxisName;
        m_yAxisName = m_freeLookCam.m_YAxis.m_InputAxisName;
    }

    private void Update()
    {
        // Toggle the freelook ability by holding the left mouse button
        if (Input.GetMouseButton(0))
        {
            m_freeLookCam.m_XAxis.m_InputAxisName = m_xAxisName;
            m_freeLookCam.m_YAxis.m_InputAxisName = m_yAxisName;
        }
        else
        {
            m_freeLookCam.m_XAxis.m_InputAxisName = "";
            m_freeLookCam.m_YAxis.m_InputAxisName = "";
            m_freeLookCam.m_XAxis.m_InputAxisValue = 0.0f;
            m_freeLookCam.m_YAxis.m_InputAxisValue = 0.0f;
        }
    }
}
