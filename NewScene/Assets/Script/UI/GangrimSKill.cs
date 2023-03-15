using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GangrimSKill : MonoBehaviour
{
    Image windFill;
    Image cloudFill;
    Image rainFill;
    
    float windSkillminGauge = 0.1f;
    float windSkillmaxGauge = 7f;

    float cloudSkillminGauge = 0.1f;
    float cloudSkillmaxGauge = 5f;

    float rainSkillminGauge = 1f;
    float rainSkillmaxGauge = 15f;

    public static float windGauge;
    public static float cloudGauge;
    public static float rainGauge;

    void Start()
    {
        
        windFill = GetComponent<Image>();
        windGauge = windSkillmaxGauge;

        cloudFill = GetComponent<Image>();
        cloudGauge = cloudSkillmaxGauge;

        rainFill = GetComponent<Image>();
        rainGauge = rainSkillmaxGauge;
    }

    void Update()
    {
        windFill.fillAmount = windGauge / windSkillminGauge;

        cloudFill.fillAmount = cloudGauge / cloudSkillminGauge;

        rainFill.fillAmount = rainGauge / rainSkillminGauge;
    }
}
