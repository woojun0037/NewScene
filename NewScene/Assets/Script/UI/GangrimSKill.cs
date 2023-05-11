using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GangrimSKill : MonoBehaviour
{
    public Image imgCoolTimeWind;
    public Image imgCoolTimeCloud;
    public Image imgCoolTimeRain;

    void Update()
    {
        WindSkillUI();
        CloudSkillUI();
        RainSkillUI();
    }

    public void WindSkillUI()
    {
        StartCoroutine(WindSkillCoolTimeCor(3f));
    }

    IEnumerator WindSkillCoolTimeCor(float cool)
    {
        while (cool > 0.1f)
        {
            cool -= Time.deltaTime;
            imgCoolTimeWind.fillAmount = (0.1f / cool);
        }
        yield return null;
    }

    public void CloudSkillUI()
    {
        StartCoroutine(CloudSkillCoolTimeCor(3f));
    }

    IEnumerator CloudSkillCoolTimeCor(float cool)
    {
        while (cool > 0.1f)
        {
            cool -= Time.deltaTime;
            imgCoolTimeCloud.fillAmount = (0.1f / cool);
        }
        yield return null;
    }

    public void RainSkillUI()
    {
        StartCoroutine(RainSkillCoolTimeCor(3f));
    }

    IEnumerator RainSkillCoolTimeCor(float cool)
    {
        while (cool > 0.1f)
        {
            cool -= Time.deltaTime;
            imgCoolTimeRain.fillAmount = (0.1f / cool);
        }
        yield return null;
    }
}
