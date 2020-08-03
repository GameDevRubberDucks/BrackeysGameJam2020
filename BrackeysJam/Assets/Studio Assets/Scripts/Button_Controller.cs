using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Controller : MonoBehaviour
{

    //--- Setup Variable ---//
    
    //--- Public Variables ---//
    public enum Type
    {
        Small,
        Medium,
        Large
    }
    public Type buttonType;

    public bool isPressed;
    public float lerpSpeed = 1.0f;

    //--- Private Variables ---//
    private Vector3 upPosition;
    private Vector3 pressedPosition;
    private float lerp = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        buttonType = Type.Small;
        upPosition = transform.position;
        pressedPosition = upPosition + new Vector3(0.0f, -0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed && lerp < 1.0f)
        {
            lerp += lerpSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(upPosition, pressedPosition, lerp);
        }
        else if (!isPressed && lerp > 0.0f)
        {
            lerp -= lerpSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(upPosition, pressedPosition, lerp);
        }
    }


    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            //Check player size, if equal to enum then

            if(playerSize == Player_SizeState.Small && buttonType == Player_SizeState.Small)
            {
                isPressed = true;
                isBeingPressed = true;
            }
            else if (playerSize == Player_SizeState.Medium && (buttonType == Player_SizeState.Small || buttonType == Player_SizeState.Medium))
            {
                isPressed = true;
                isBeingPressed = true;
            }
            else if (playerSize == Player_SizeState.Large && (buttonType == Player_SizeState.Small || buttonType == Player_SizeState.Medium || buttonType == Player_SizeState.Large))
            {
                isPressed = true;
                isBeingPressed = true;
            }
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPressed = false;
        }
    }

}
