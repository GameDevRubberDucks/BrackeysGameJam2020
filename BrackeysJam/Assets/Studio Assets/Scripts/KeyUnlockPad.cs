using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyUnlockPad : MonoBehaviour
{
    public int lockNumber;
    public Transform door;

    //private var
    private Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        //store the inital position of the object
        initPosition = door.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player_ItemInteractions>().itemOnHold != null)
        {
            other.gameObject.GetComponent<Player_ItemInteractions>().itemOnHold.dropItem();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        //check if the item is on the lockpad and if the key number matches up with the lock number
        if (other.gameObject.tag == "Item")
        {
            if (other.GetComponent<Item_behavior>().keyNumber == lockNumber && door.position.y >= -4.0f)
            {
                door.position = Vector3.MoveTowards(door.position, new Vector3(door.position.x, door.position.y - 4.0f, door.position.z), 0.1f);
            }
        }
    }


    //methods
    public void ObjectReset()
    {
        //reset the door to its initial position
        door.position = initPosition;
    }
}

