using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GangrimSKill : MonoBehaviour
{
    public Image imgCoolTimeWind;
    public Image imgCoolTimeCloud;
    public Image imgCoolTimeRain;

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
        
    }

    void Update()
    {
        WindSkillUI();
        CloudSkillUI();
        RainSkillUI();
    }

    public void WindSkillUI()
    {
        while (windSkillminGauge <= windGauge)
        {
            imgCoolTimeWind.fillAmount = windGauge / windSkillminGauge;
            windSkillminGauge += 0.1f;
        }
        imgCoolTimeWind.fillAmount = 0;
    }

    public void CloudSkillUI()
    {

        //cloudFill.fillAmount = cloudGauge / cloudSkillminGauge;
    }

    public void RainSkillUI()
    {

        //rainFill.fillAmount = rainGauge / rainSkillminGauge;
    }



}
