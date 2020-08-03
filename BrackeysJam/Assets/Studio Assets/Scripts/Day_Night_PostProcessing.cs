using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Day_Night_PostProcessing : MonoBehaviour
{

    public Volume volume;
    public Color dayColor = new Color(255.0f, 215.0f, 194.0f);
    public Color nightColor = new Color(71.0f, 67.0f, 174.0f);

    private ColorAdjustments color;

    private float dayIntensity;
    private float nightIntensity;


    // Start is called before the first frame update
    void Awake()
    {
        FindObjectOfType<Day_Controller>().OnDayTimeUpdated.AddListener(this.OnLightUpdated);
        volume.profile.TryGet(out color);
        dayIntensity   = (dayColor.r + dayColor.g + dayColor.b) / 3.0f;
        nightIntensity = (nightColor.r + nightColor.g + nightColor.b) / 3.0f;
        dayColor /= dayIntensity;
        nightColor /= nightIntensity;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnLightUpdated(float _newDayPercent)
    {
        color.colorFilter.value = Color.Lerp(dayColor, nightColor, _newDayPercent);
    }
}
