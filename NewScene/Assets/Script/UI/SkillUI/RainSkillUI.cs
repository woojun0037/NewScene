using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainSkillUI : MonoBehaviour
{
    Image rainFill;

    float rainSkillminGauge = 1f;
    float rainSkillmaxGauge = 15f;

    public static float rainGauge;

    void Start()
    {
        rainFill = GetComponent<Image>();
        rainGauge = rainSkillminGauge;
    }

    void Update()
    {
        
    }

    public void rainSkillCool()
    {
        rainFill.fillAmount = rainGauge / rainSkillmaxGauge;
    }
}
