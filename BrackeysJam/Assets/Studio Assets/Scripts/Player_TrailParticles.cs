using UnityEngine;

public class Player_TrailParticles : MonoBehaviour
{
    //--- Public Variables ---//
    [Header("Renderables")]
    public ParticleSystem m_particles;
    public Material m_baseMat;
    public Material m_electrifiedMat;

    [Header("Raycast Spawning")]
    public LayerMask m_groundRayMask;
    public float m_groundOffset;



    //--- Private Variables ---//
    private Player_SizeController m_sizeController;
    private ParticleSystemRenderer m_particleRend;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_sizeController = GetComponent<Player_SizeController>();
        m_particleRend = m_particles.GetComponent<ParticleSystemRenderer>();

        // Find all of the power box puzzles in the scene and register for their completion event
        // This way, we can disable the electrified trail effect whenever we complete a power box puzzle
        var powerBoxPuzzles = FindObjectsOfType<PowerBox_Communicator>();
        foreach (var powerBoxPuzzle in powerBoxPuzzles)
            powerBoxPuzzle.onPowerBoxesConnected.AddListener(this.EndElectrifiedTrail);
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

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we collided with a power box
        if (collision.gameObject.TryGetComponent<PowerBox_Controller>(out var powerBox))
        {
            // If the powerbox is not already active, we should trigger the electricity
            if (!powerBox.m_isActive)
                m_particleRend.material = m_electrifiedMat;
        }
    }



    //--- Methods ---//
    public void ResetTrail()
    {
        // Clear all of the particles currently in the particle system
        m_particles.Clear();

        // Go back to the normal material
        m_particleRend.material = m_baseMat;
    }

    public void EndElectrifiedTrail()
    {
        // Go back to the basic trail material
        m_particleRend.material = m_baseMat;
    }
}
