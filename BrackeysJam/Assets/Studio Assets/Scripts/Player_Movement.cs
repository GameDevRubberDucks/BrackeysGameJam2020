using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //--- Public Variables ---//
    public Transform m_mainCam;
    public float m_moveForceStr;
    public bool m_stopWhenNoInput;



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
        if (m_stopWhenNoInput && (hAxis == 0.0f && vAxis == 0.0f))
        {
            var currentVelY = m_rb.velocity.y;
            m_rb.velocity = new Vector3(0.0f, currentVelY, 0.0f);
            m_rb.angularVelocity = Vector3.zero;
            return;
        }

        // Create a normalized input vector to avoid having double speed when moving diagonally
        Vector2 inputVector = new Vector2(hAxis, vAxis);
        inputVector.Normalize();

        // Determine the new force vector
        float xSpeed = inputVector.x * m_moveForceStr * Time.deltaTime;
        float zSpeed = inputVector.y * m_moveForceStr * Time.deltaTime;
        Vector3 newForce = new Vector3(xSpeed, 0.0f, zSpeed);

        // Transform the force so it is relative to the camera
        Vector3 transformedForce = m_mainCam.TransformDirection(newForce);

        // Move the ball
        m_rb.AddForce(transformedForce);
    }
}
