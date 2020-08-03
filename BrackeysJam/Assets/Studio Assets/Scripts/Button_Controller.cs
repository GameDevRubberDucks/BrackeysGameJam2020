using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button_Controller : MonoBehaviour
{

    //--- Setup Variable ---//

    //--- Public Variables ---//
    public Player_SizeState buttonType;
    public bool isPressed;
    public bool isBeingPressed;
    public float lerpSpeed = 1.0f;
    public UnityEvent onButtonPressed;

    //--- Private Variables ---//
    private Vector3 upPosition;
    private Vector3 pressedPosition;
    private float lerp = 0.0f;
    private bool canFireEvent = false;
    // Start is called before the first frame update
    void Start()
    {
        //buttonType = Player_SizeState.Large;
        upPosition = transform.position;
        pressedPosition = upPosition + new Vector3(0.0f, -0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingPressed && lerp < 1.0f)
        {
            lerp += lerpSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(upPosition, pressedPosition, lerp);
        }
        else if (!isBeingPressed && lerp > 0.0f)
        {
            lerp -= lerpSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(upPosition, pressedPosition, lerp);
        }
    }


    private void OnCollisionEnter(Collision col)
    {
        Player_SizeState playerSize = col.gameObject.GetComponent<Player_SizeController>().GetCurrentSizeState();
        if (col.gameObject.tag == "Player")
        {
            //Check player size, if equal to enum then

            if(playerSize == Player_SizeState.Small && buttonType == Player_SizeState.Small)
            {
                isPressed = true;
                isBeingPressed = true;

                if (canFireEvent)
                {
                    onButtonPressed.Invoke();
                    canFireEvent = false;
                }
            }
            else if (playerSize == Player_SizeState.Medium && (buttonType == Player_SizeState.Small || buttonType == Player_SizeState.Medium))
            {
                isPressed = true;
                isBeingPressed = true;

                if (canFireEvent)
                {
                    onButtonPressed.Invoke();
                    canFireEvent = false;
                }
            }
            else if (playerSize == Player_SizeState.Large && (buttonType == Player_SizeState.Small || buttonType == Player_SizeState.Medium || buttonType == Player_SizeState.Large))
            {
                isPressed = true;
                isBeingPressed = true;

                if (canFireEvent)
                {
                    onButtonPressed.Invoke();
                    canFireEvent = false;
                }
            }
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            isBeingPressed = false;
            canFireEvent = true;
        }
    }

}
