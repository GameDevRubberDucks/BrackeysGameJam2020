using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_ItemInteractions : MonoBehaviour
{
    //is true is holding key item.
    public bool holdItem;
    public Image itemSlot;

    //private variables
    private Item_behavior itemOnHold;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //update item slot cd

        if (itemOnHold != null && !itemOnHold.isHolding)
        {
            itemSlot.enabled = false;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        //check if coliding with item
        if (other.gameObject.tag == "Item")
        {
            //player is now holding a key
            holdItem = true;
            itemOnHold = other.gameObject.GetComponent<Item_behavior>();


            //disable the item prefab
            itemOnHold.hideitem();

            itemSlot.enabled = true;
            itemOnHold.isHolding = true;
        }
    }

}
