using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public string ParameterName = "";
    private Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        float SliderValue;
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName(ParameterName, out SliderValue);
        slider.value = SliderValue;
    }

    // Update is called once per frame
    public void SetVolume(float SliderValue)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(ParameterName, SliderValue);
    }
}
