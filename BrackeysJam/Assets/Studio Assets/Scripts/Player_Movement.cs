using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //--- Public Variables ---//
    public Transform m_mainCam;
    public float m_movementSpeed;



    //--- Private Variables ---//
    private Rigidbody m_rb;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get the inputs
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // If there is no input, we should stop all velocity except for gravity
        if (hAxis == 0.0f && vAxis == 0.0f)
        {
            var currentVelY = m_rb.velocity.y;
            m_rb.velocity = new Vector3(0.0f, currentVelY, 0.0f);
            return;
        }

        // Create a normalized input vector to avoid having double speed when moving diagonally
        Vector2 inputVector = new Vector2(hAxis, vAxis);
        inputVector.Normalize();

        // Determine the new velocity vector
        float xSpeed = inputVector.x * m_movementSpeed;
        float zSpeed = inputVector.y * m_movementSpeed;
        Vector3 newVel = new Vector3(xSpeed, 0.0f, zSpeed);

        // Transform the velocity so it is relative to the camera
        Vector3 transformedVel = m_mainCam.TransformDirection(newVel);

        // Use the pre-existing y-velocity to ensure that gravity is still applied
        transformedVel.y = m_rb.velocity.y;

        // Move the ball
        m_rb.velocity = transformedVel;
    }
}
