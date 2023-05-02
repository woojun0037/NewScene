using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindSkillUI : MonoBehaviour
{
    Image windFill;

    //float windSkillminGauge = 0.1f;
    float windSkillmaxGauge = 7f;

    public static float windGauge;

    void Start()
    {
        windFill = GetComponent<Image>();
        windGauge = windSkillmaxGauge;
    }

   
    void Update()
    {
        windFill.fillAmount = Mathf.Sin(windGauge) * 0.5f + 0.5f;
    }

   
    
}
