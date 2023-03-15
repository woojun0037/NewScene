using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudSkillUI : MonoBehaviour
{
    Image cloudFill;

    float cloudSkillminGauge = 0.1f;
    float cloudSkillmaxGauge = 5f;

    public static float cloudGauge;

    void Start()
    {
        cloudFill = GetComponent<Image>();
        cloudGauge = cloudSkillminGauge;
    }

    
    void Update()
    {
        
    }

    public void cloudSkillCool()
    {
        cloudFill.fillAmount = cloudGauge / cloudSkillmaxGauge;
    }

}
