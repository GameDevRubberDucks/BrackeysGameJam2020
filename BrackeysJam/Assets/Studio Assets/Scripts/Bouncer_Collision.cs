using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer_Collision : MonoBehaviour
{
    //--- Public Variables ---//

    public float bouncerForce = 1000.0f;

    private float forceScaler = 1.0f;
    private Player_SizeController currentSize;

    // Start is called before the first frame update
    void Start()
    {
        currentSize = GameObject.FindObjectOfType<Player_SizeController>().GetComponent<Player_SizeController>();
    }

    // Update is called once per frame
    void Update()
    {
        forceScaler = currentSize.PercentOfMaxSize;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("SPRING");

            float rescaledForce = bouncerForce * (1 + (1 - forceScaler));
            //float rescaledForce = bouncerForce;
            collision.rigidbody.AddForce(this.transform.up * rescaledForce, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("SPRING");
            other.GetComponent<Rigidbody>().AddForce(this.transform.up * bouncerForce * forceScaler, ForceMode.Impulse);
        }
    }
}
