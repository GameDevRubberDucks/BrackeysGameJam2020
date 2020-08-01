using UnityEngine;

public class Player_Respawner : MonoBehaviour
{
    //--- Private Variables ---//
    private Respawn_Zone m_currentRespawnZone;



    //--- Unity Methods ---//
    private void OnTriggerEnter(Collider other)
    {
        // When colliding with a new respawn point, we should keep track of it
        if (other.tag == "Respawn_ZoneTrigger")
            RegisterNewRespawnPoint(other.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            this.Respawn();
    }



    //--- Methods ---//
    public void Respawn()
    {
        // Ensure we have a respawn point to go to
        if (m_currentRespawnZone == null)
        {
            Debug.LogError("Error: No respawn zone set!");
            return;
        }

        // Move to the respawn point's position and orientation
        this.transform.position = m_currentRespawnZone.m_respawnPoint.position;
        this.transform.rotation = m_currentRespawnZone.m_respawnPoint.rotation;

        // TODO: Reset the size of the ball
    }



    //--- Utility Functions ---//
    private void RegisterNewRespawnPoint(GameObject _zoneObj)
    {
        // Firstly, we should tell the previous respawn point that it is no longer active
        if (m_currentRespawnZone != null)
            m_currentRespawnZone.IsActive = false;

        // Store the new zone's script and activate it
        var zoneScript = _zoneObj.GetComponentInParent<Respawn_Zone>();
        m_currentRespawnZone = zoneScript;
        m_currentRespawnZone.IsActive = true;
    }
}
