using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SiderScript : MonoBehaviour
{
    public Slider slider;
    public CameraMovemant cam;

    CameraMovemant cameSensitivity;

    public void OnEnable()
    {
       slider.value = cam.sensitivity;
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }


    public void OnSliderValueChanged(float value)
    {
        cam.sensitivity = value;
    }

}
