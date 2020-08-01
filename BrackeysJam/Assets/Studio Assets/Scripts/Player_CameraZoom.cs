using UnityEngine;
using Cinemachine;

public class Player_CameraZoom : MonoBehaviour
{
    //--- Public Variables ---//
    public CinemachineFreeLook m_freeLookCam;
    //public float m_minFOV;
    //public float m_maxZoomOutFOV;
    public float m_midRigMinRadius;
    public float m_midRigMinHeight;



    //--- Private Variables ---//
    private Player_SizeController m_sizeController;
    private float m_midRigStartRadius;
    private float m_midRigStartHeight;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_sizeController = GetComponent<Player_SizeController>();
        var midRigOrbit = m_freeLookCam.m_Orbits[1];
        m_midRigStartRadius = midRigOrbit.m_Radius;
        m_midRigStartHeight = midRigOrbit.m_Height;

        // Register for necessary events
        m_sizeController.OnSizeUpdated.AddListener(UpdateZoom);
    }



    //--- Methods ---//
    public void UpdateZoom(float _sizePercentage)
    {
        if (this.enabled)
        {
            // Close the camera orbits in around the player to zoom in
            float zoomT = 1.0f - _sizePercentage;
            float newMidRigRadius = Mathf.Lerp(m_midRigStartRadius, m_midRigMinRadius, zoomT);
            float newMidRigHeight = Mathf.Lerp(m_midRigStartHeight, m_midRigMinHeight, zoomT);

            // Apply the updated orbit values to the camera
            var updatedOrbit = new CinemachineFreeLook.Orbit();
            updatedOrbit.m_Height = newMidRigHeight;
            updatedOrbit.m_Radius = newMidRigRadius;
            m_freeLookCam.m_Orbits[1] = updatedOrbit;
        }
    }
}
