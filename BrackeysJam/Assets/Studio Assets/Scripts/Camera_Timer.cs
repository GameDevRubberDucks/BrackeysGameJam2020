using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Timer : MonoBehaviour
{
    //Public Variables
    public float shotDuration;
    public bool startTimer = false;

    private Rigidbody rb_Player;
    private float storedMoveForce;
    private float timer;
    [SerializeField] private bool alreadyShown = false;

    // Start is called before the first frame update
    void Start()
    {
        rb_Player = GameObject.FindObjectOfType<Player_Movement>().GetComponent<Rigidbody>();

        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;

            if (timer >= shotDuration)
                StopCameraShot();

        }
    }

    public void StartShotTimer()
    {
        if (!alreadyShown)
        {
            startTimer = true;

            rb_Player.isKinematic = true;
        }
        
    }

    private void StopCameraShot()
    {
        startTimer = false;
        timer = 0.0f;

        rb_Player.isKinematic = false;

        gameObject.GetComponent<Camera>().enabled = false;
        alreadyShown = true;
    }
}
