using UnityEngine;
using UnityEngine.UI;

public class UI_DayNightSystem : MonoBehaviour
{
    //--- Public Variables ---//
    public Slider m_sldDayNight;

    //--- Unity Methods ---//
    private void Awake()
    {
        // Register for the event
        FindObjectOfType<Day_Controller>().OnDayTimeUpdated.AddListener(this.OnDayPercentageUpdated); 
    }

    //--- Methods ---//
    public void OnDayPercentageUpdated(float _newDayPercent)
    {
        // Update the slider to represent the passage of time
        m_sldDayNight.value = _newDayPercent;
    }

}
