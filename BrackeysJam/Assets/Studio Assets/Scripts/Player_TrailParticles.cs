using UnityEngine;
using System.Collections;

public class Player_TrailParticles : MonoBehaviour
{
    //--- Public Variables ---//
    public ParticleSystem m_particles;
    public float m_groundRaycastHeight;
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
        // Align the particle system to the normal of the surface below
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, m_groundRaycastHeight, m_groundRayMask))
        {
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.position = hit.point + (hit.normal * m_groundOffset);
            emitParams.rotation3D = Quaternion.LookRotation(-hit.normal).eulerAngles;
            emitParams.startSize = m_sizeController.GetCurrentSize();
            m_particles.Emit(emitParams, 1);
        }
    }

    //--- Utility Methods ---//
    private bool IsOnGround()
    {
        // Raycast down to check if the player is on the ground
        return Physics.Raycast(transform.position, Vector3.down, m_groundRaycastHeight, m_groundRayMask);
    }

    private float GetAngleBetweenNormalAndUp(Vector3 _normal)
    {
        Vector3 normalized = _normal.normalized;
        float angle = Vector3.Angle(_normal, Vector3.up);
        return Mathf.Deg2Rad * angle;
    }

    private void ApplyLookRotation(ParticleSystem.MainModule _main, Vector3 _normal)
    {
        Quaternion lookRot = Quaternion.LookRotation(_normal);
        Vector3 lookRotEuler = lookRot.eulerAngles;
        _main.startRotationX = lookRotEuler.x;
        _main.startRotationY = lookRotEuler.y;
        _main.startRotationZ = lookRotEuler.z;
    }
}
