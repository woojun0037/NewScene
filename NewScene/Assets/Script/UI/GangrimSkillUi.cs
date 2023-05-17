using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GangrimSkillUi : MonoBehaviour
{
    [Header("WindSKill")]
    public Image abilityImage1;
    public float coolDown1 = 5;
    bool isWindSKillCoolDown = false;
    public KeyCode WindSKill;

    [Header("CloudSKill")]
    public Image abilityImage2;
    public float coolDown2 = 3;
    bool isCloudSKillCoolDown = false;
    public KeyCode CloudSKill;

    [Header("RainSkill")]
    public Image abilityImage3;
    public float coolDown3 = 6;
    bool isRainSkillCoolDown = false;
    public KeyCode RainSkill;

    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
    }

    void Update()
    {
        WindSKillAbilty();
        CloudSKillAbilty();
        RainSkillAbilty();
    }

    void WindSKillAbilty()
    {
        if(Input.GetKey(WindSKill) && isWindSKillCoolDown == false)
        {
            isWindSKillCoolDown = true;
            abilityImage1.fillAmount = 1;
        }

        if(isWindSKillCoolDown)
        {
            abilityImage1.fillAmount -= 1 / coolDown1 * Time.deltaTime;

            if(abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isWindSKillCoolDown = false;
            }
        }
    }

    void CloudSKillAbilty()
    {
        if (Input.GetKey(CloudSKill) && isCloudSKillCoolDown == false)
        {
            isCloudSKillCoolDown = true;
            abilityImage2.fillAmount = 1;
        }

        if (isCloudSKillCoolDown)
        {
            abilityImage2.fillAmount -= 1 / coolDown2 * Time.deltaTime;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCloudSKillCoolDown = false;
            }
        }
    }

    void RainSkillAbilty()
    {
        if (Input.GetKey(RainSkill) && isRainSkillCoolDown == false)
        {
            isRainSkillCoolDown = true;
            abilityImage3.fillAmount = 1;
        }

        if (isRainSkillCoolDown)
        {
            abilityImage3.fillAmount -= 1 / coolDown3 * Time.deltaTime;

            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isRainSkillCoolDown = false;
            }
        }
    }
}
