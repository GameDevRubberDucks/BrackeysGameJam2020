using UnityEngine;

public class Heatpad_Controller : MonoBehaviour
{
    //--- Public Variables ---//
    public float m_cooldownDuration;
    public Color m_cooldownColour;



    //--- Private Variables ---//
    private Renderer m_renderer;
    private Color m_baseColour;
    private bool m_isCoolingDown;
    private float m_timeSinceCooldown;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_renderer = GetComponent<Renderer>();
        m_baseColour = m_renderer.material.color;
        m_isCoolingDown = false;
        m_timeSinceCooldown = 0.0f;

        // Register for the day reset event
        FindObjectOfType<Day_Controller>().OnDayReset.AddListener(this.ResetHeatpad);
    }

    private void Update()
    {
        if (m_isCoolingDown)
        {
            // Increase the timer
            m_timeSinceCooldown += Time.deltaTime;

            // Check if the cooldown should be finished
            if (m_timeSinceCooldown >= m_cooldownDuration)
                EndCooldown();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only consider collisions when not in cooldown mode
        if (!m_isCoolingDown)
        {
            // Try to grab the player's size script
            if (other.TryGetComponent<Player_SizeController>(out var playerSize))
            {
                // Drop the player down a size
                playerSize.DropToNextSizeStage();

                // Start cooling down
                StartCooldown();
            }
        }
    }



    //--- Methods ---//
    public void StartCooldown()
    {
        // Start the timer
        m_isCoolingDown = true;
        m_timeSinceCooldown = 0.0f;

        // Change the colour to indicate the start of cooldown
        m_renderer.material.color = m_cooldownColour;
    }

    public void EndCooldown()
    {
        // End the timer
        m_isCoolingDown = false;

        // Change the cooldown to indicate the end of the cooldown
        m_renderer.material.color = m_baseColour;
    }

    public void ResetHeatpad()
    {
        // Reset to the base state
        m_renderer.material.color = m_baseColour;
        m_isCoolingDown = false;
    }
}
