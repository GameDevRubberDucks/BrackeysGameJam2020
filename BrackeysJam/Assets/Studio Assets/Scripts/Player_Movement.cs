using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //--- Public Variables ---//
    public Camera m_mainCam;
    public float m_movementSpeed;



    //--- Private Variables ---//
    private Rigidbody m_rb;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Get the inputs
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // Let gravity take over if there is no input
        if (hAxis == 0.0f && vAxis == 0.0f)
            return;

        // Determine the new velocity vector
        float xSpeed = hAxis * m_movementSpeed;
        float zSpeed = vAxis * m_movementSpeed;
        Vector3 newVel = new Vector3(xSpeed, 0.0f, zSpeed);

        // Transform the velocity so it is relative to the camera
        Vector3 transformedVel = m_mainCam.transform.TransformDirection(newVel);
        transformedVel.y = 0.0f;

        // Move the ball
        m_rb.velocity = transformedVel;
    }
}
