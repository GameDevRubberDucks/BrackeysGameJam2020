using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Item_behavior : MonoBehaviour
{
    //public vars
    //the percentage of size before the item drops
    public float itemDropPoint;
    public GameObject model;
    public bool isHolding;

    //private vars
    private Vector3 currentPos;
    private Transform playerTF;
    private Player_SizeController playerSize;

    private Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        playerTF = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerSize = GameObject.FindObjectOfType<Player_SizeController>();
        isHolding = false;

    }
    private void Update()
    {
        if(playerSize.PercentOfMaxSize <= itemDropPoint && isHolding)
        {
            currentPos = playerTF.position;
            dropItem();
        }
    }

    public void hideitem()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        model.SetActive(false);
    }

    //reset object location and active state
    public void rewind()
    {
        transform.position = initPos;
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    //drop the item at the players current position
    public void dropItem()
    {
        model.SetActive(true);
        transform.position = currentPos;
        isHolding = false;
    }

    //corrent position setter
    public void setCurrentPos(Vector3 pos)
    {
        currentPos = pos;
    }

    // current position getter
    public Vector3 getCurretnPos()
    {
        return currentPos;
    }


}
