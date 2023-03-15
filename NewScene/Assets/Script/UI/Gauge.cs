using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    Image GaugePill;

    public float minGauge = 1f;
    public float maxGauge = 30f;

    public static float sGauge;

    void Start()
    {
        GaugePill = GetComponent<Image>();
        sGauge = minGauge;
    }

    
    void Update()
    {
        GaugePill.fillAmount = sGauge / maxGauge;
    }
}
