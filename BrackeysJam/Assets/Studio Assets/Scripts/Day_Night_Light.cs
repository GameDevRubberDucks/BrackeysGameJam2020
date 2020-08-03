using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Day_Night_Light : MonoBehaviour
{
    public Transform day;
    public Transform night;

    // Start is called before the first frame update
    void Awake()
    {
        FindObjectOfType<Day_Controller>().OnDayTimeUpdated.AddListener(this.OnLightUpdated);
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void OnLightUpdated(float _newDayPercent)
    {
       transform.rotation = Quaternion.Lerp(day.rotation, night.rotation, _newDayPercent);
    }
}
