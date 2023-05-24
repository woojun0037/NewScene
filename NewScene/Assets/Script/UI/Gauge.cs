using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Image Dark_gauge;
    public PlayerSkill skill;
    public float minGauge = 1f;
    public float maxGauge = 30f;
    public bool dark_gauge_check;

    public static float sGauge;

    void Start()
    {
        Dark_gauge = GetComponent<Image>();
        sGauge = minGauge;
    }

    
    void Update()
    {
        DarkSkillsystem();
    }


    public static void Test()
    {
        Debug.Log(sGauge);
    }
    public void DarkSkillsystem()
    {
        Dark_gauge.fillAmount = sGauge / maxGauge;
    }

    public void CorStart()
    {
        StartCoroutine(DarkGaugeFillMinus());
    }

    public IEnumerator DarkGaugeFillMinus()
    {
        while(sGauge > 0)
        {
            sGauge -= 0.1f;
            yield return new WaitForSeconds(0.03f);
        }

        dark_gauge_check = false;
    }
}
