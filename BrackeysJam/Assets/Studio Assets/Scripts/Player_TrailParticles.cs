using UnityEngine;
using System.Collections;

public class Player_TrailParticles : MonoBehaviour
{
    //--- Public Variables ---//
    public ParticleSystem m_particles;
    public LayerMask m_groundRayMask;
    public float m_groundOffset;



    //--- Private Variables ---//
    private Player_SizeController m_sizeController;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_sizeController = GetComponent<Player_SizeController>();
    }

    private void Update()
    {
        // Use the height of the player to determine how large the raycast should be
        float rayLength = m_sizeController.GetCurrentRadius();

        // Align the particle system to the normal of the surface below
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, rayLength, m_groundRayMask))
        {
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.position = hit.point + (hit.normal * m_groundOffset);
            emitParams.rotation3D = Quaternion.LookRotation(-hit.normal).eulerAngles;
            emitParams.startSize = m_sizeController.GetCurrentRadius();
            m_particles.Emit(emitParams, 1);
        }
    }



    //--- Methods ---//
    public void ResetTrail()
    {
        // Clear all of the particles currently in the particle system
        m_particles.Clear();
    }
}
