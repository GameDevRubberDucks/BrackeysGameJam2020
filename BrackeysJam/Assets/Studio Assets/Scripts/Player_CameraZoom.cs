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

    public float m_topRigMinRadius;
    public float m_topRigMinHeight;

    public float m_botRigMinRadius;
    public float m_botRigMinHeight;



    //--- Private Variables ---//
    private Player_SizeController m_sizeController;
    private float m_midRigStartRadius;
    private float m_midRigStartHeight;

    private float m_topRigStartRadius;
    private float m_topRigStartHeight;

    private float m_botRigStartRadius;
    private float m_botRigStartHeight;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_sizeController = GetComponent<Player_SizeController>();
        var midRigOrbit = m_freeLookCam.m_Orbits[1];
        m_midRigStartRadius = midRigOrbit.m_Radius;
        m_midRigStartHeight = midRigOrbit.m_Height;


        var topRigOrbit = m_freeLookCam.m_Orbits[2];
        m_topRigStartHeight = topRigOrbit.m_Height;
        m_topRigStartRadius = topRigOrbit.m_Radius;


        var botRigOrbit = m_freeLookCam.m_Orbits[0];
        m_botRigStartRadius = botRigOrbit.m_Radius;
        m_botRigStartHeight = botRigOrbit.m_Height;

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

            float newTopRigRadius = Mathf.Lerp(m_topRigStartRadius, m_topRigMinRadius, zoomT);
            float newTopRigHeight = Mathf.Lerp(m_topRigStartHeight, m_topRigMinHeight, zoomT);

            float newBotRigRadius = Mathf.Lerp(m_botRigStartRadius, m_botRigMinRadius, zoomT);
            float newBotRigHeight = Mathf.Lerp(m_botRigStartHeight, m_botRigMinHeight, zoomT);

            // Apply the updated orbit values to the camera
            var updatedMidOrbit = new CinemachineFreeLook.Orbit();
            updatedMidOrbit.m_Height = newMidRigHeight;
            updatedMidOrbit.m_Radius = newMidRigRadius;


            var updatedTopOrbit = new CinemachineFreeLook.Orbit();
            updatedTopOrbit.m_Height = newTopRigHeight;
            updatedTopOrbit.m_Radius = newTopRigRadius;

            var updatedBotOrbit = new CinemachineFreeLook.Orbit();
            updatedBotOrbit.m_Height = newBotRigHeight;
            updatedBotOrbit.m_Radius = newBotRigRadius;


            m_freeLookCam.m_Orbits[1] = updatedMidOrbit;
            m_freeLookCam.m_Orbits[2] = updatedTopOrbit;
            m_freeLookCam.m_Orbits[0] = updatedBotOrbit;
        }
    }
}
