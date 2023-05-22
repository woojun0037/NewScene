using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Image Dark_gauge;

    public float minGauge = 1f;
    public float maxGauge = 30f;

    public static float sGauge;

    void Start()
    {
        Dark_gauge = GetComponent<Image>();
        sGauge = minGauge;
    }

    
    void Update()
    {
        Dark_gauge.fillAmount = sGauge / maxGauge;
    }
}
