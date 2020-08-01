using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSize : MonoBehaviour
{
    //--- Public Variables ---//
    public Slider m_sldPlayerSize;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Register for necessary data update events
        var sizeController = FindObjectOfType<Player_SizeController>();
        var eventFunc = sizeController.OnSizeUpdated;
        eventFunc.AddListener(this.UpdatePlayerSizeVis);
    }



    //--- Methods ---//
    public void UpdatePlayerSizeVis(float _newPercentage)
    {
        // Update the slider so that it matches the current size amount
        m_sldPlayerSize.value = _newPercentage;
    }
}
