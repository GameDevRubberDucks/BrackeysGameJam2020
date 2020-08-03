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
    public Item_behavior itemOnHold;

    //private vars
    private Collider TempItemHolder;

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
        //check if coliding with item, and if it is a item that was just droped 
        if (other.gameObject.tag == "Item" && TempItemHolder != other)
        {
            //player is now holding a key
            holdItem = true;
            itemOnHold = other.gameObject.GetComponent<Item_behavior>();
            //hold the reference to the current item
            TempItemHolder = other;

            //disable the item prefab
            itemOnHold.hideitem();

            itemSlot.enabled = true;
            itemOnHold.isHolding = true;
        }
    }

}
