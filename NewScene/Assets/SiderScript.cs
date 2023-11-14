using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SiderScript : MonoBehaviour
{
    public Slider slider;
    public CameraMovemant cam;

    public void OnEnable()
    {
        slider.value = cam.sensitivity;
    }

    private void Update()
    {
        SlideScroll();
    }

    public void SlideScroll()
    {
        slider.value = slider.minValue/slider.maxValue;
        cam.sensitivity = slider.value;
    }
}
