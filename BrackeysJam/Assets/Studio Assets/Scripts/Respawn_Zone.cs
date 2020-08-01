using UnityEngine;

public class Respawn_Zone : MonoBehaviour
{
    //--- Public Variables ---//
    public Transform m_respawnPoint;
    public GameObject m_activeIndicator;



    //--- Properties ---//
    public bool IsActive
    {
        set => m_activeIndicator.SetActive(value);
    }
}
