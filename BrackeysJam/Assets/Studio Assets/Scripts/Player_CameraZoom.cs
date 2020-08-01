using UnityEngine;
using Cinemachine;

public class Player_CameraZoom : MonoBehaviour
{
    //--- Public Variables ---//
    public CinemachineFreeLook m_freeLookCam;
    public float m_minFOV;
    public float m_maxZoomOutFOV;



    //--- Private Variables ---//
    private Player_SizeController m_sizeController;
    private float m_startFOV;
    private bool m_useMaxZoomOut;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_sizeController = GetComponent<Player_SizeController>();
        m_startFOV = m_freeLookCam.m_Lens.FieldOfView;
        UseMaxZoomOut = false;

        // Register for necessary events
        m_sizeController.OnSizeUpdated.AddListener(UpdateZoom);
    }

    private void Update()
    {
        // Toggle the max zoom out by pressing space
        if (Input.GetKeyDown(KeyCode.Space))
            UseMaxZoomOut = !UseMaxZoomOut;            
    }



    //--- Methods ---//
    public void UpdateZoom(float _sizePercentage)
    {
        // Lerp the field of view to mimic zooming in
        float zoomT = 1.0f - _sizePercentage;
        float newFOV = Mathf.Lerp(m_startFOV, m_minFOV, zoomT);
        m_freeLookCam.m_Lens.FieldOfView = newFOV;
    }


    
    //--- Properties ---//
    public bool UseMaxZoomOut
    {
        get => m_useMaxZoomOut;
        set
        {
            m_useMaxZoomOut = value;

            if (m_useMaxZoomOut == true)
            {
                // Unregister from the player size change event
                m_sizeController.OnSizeUpdated.RemoveListener(this.UpdateZoom);

                // Update the FOV to be the zoomed out version
                m_freeLookCam.m_Lens.FieldOfView = m_maxZoomOutFOV;
            }
            else
            {
                // Register for the player size change event
                m_sizeController.OnSizeUpdated.AddListener(this.UpdateZoom);

                // Trigger the update manually here
                UpdateZoom(m_sizeController.PercentOfMaxSize);
            }
        }
    }
}
