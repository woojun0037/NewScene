using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GangrimSkillUi : MonoBehaviour
{
    public HealthManager curHp;
    public Gauge DarkPill;
    public GameObject player;
    public PlayerSkill playerSkillCheck;
    public PropertySkill PropertySkillCheck;

    public bool LWindC;
    public bool LCloudC;
    public bool LRainC;

    public bool RWindC;
    public bool RCloudC;
    public bool RRainC;

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

    [Header("ItemList")]
    public GameObject HP_item;
    public KeyCode HP_itemKey;
    public GameObject RESPWAN_item;
    public KeyCode Respwan_ItemKey;
    public GameObject DARKPILL_item;
    public KeyCode DarkPillItem__itemkey;

    [Header("SkillUseList")]
    [Header("Left")]
    public GameObject LeftWindUsing;
    public GameObject LeftCloudUsing;
    public GameObject LeftRainUsing;
    [Header("Right")]
    public GameObject RightWindUsing;
    public GameObject RightCloudUsing;
    public GameObject RightRainUsing;

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
        ItemUse();
        //LeftUseSkill();
        //RightUseSkill();
    }

    public void WindSKillAbilty()
    {
        if (Input.GetKey(WindSKill) && isWindSKillCoolDown == false)
        {
            isWindSKillCoolDown = true;
            abilityImage1.fillAmount = 1;
        }

        if (isWindSKillCoolDown)
        {
            abilityImage1.fillAmount -= 1 / coolDown1 * Time.deltaTime;

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isWindSKillCoolDown = false;
            }
        }
    }
    public void CloudSKillAbilty()
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
    public void RainSkillAbilty()
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
    //public void LeftUseSkill()
    //{
    //    if (playerSkillCheck.WindSkillCheck && LWindC)
    //    {
    //        LeftWindUsing.SetActive(true);
    //        RWindC = false;
    //    }
    //    else if (playerSkillCheck.E_skillCheck || playerSkillCheck.R_skillCheck)
    //    {
    //        LeftWindUsing.SetActive(false);
    //        RWindC = true;
    //    }

    //    if (playerSkillCheck.E_skillCheck && LCloudC)
    //    {
    //        LeftCloudUsing.SetActive(true);
    //        LCloudC = false;
    //    }
    //    else if (playerSkillCheck.WindSkillCheck || playerSkillCheck.R_skillCheck)
    //    {
    //        LeftCloudUsing.SetActive(false);
    //        RCloudC = true;
    //    }

    //    if (playerSkillCheck.R_skillCheck && LRainC)
    //    {
    //        LeftRainUsing.SetActive(true);
    //        RRainC = false;
    //    }
    //    else if (playerSkillCheck.WindSkillCheck || playerSkillCheck.E_skillCheck)
    //    {
    //        LeftRainUsing.SetActive(false);
    //        RRainC = true;
    //    }
    //}

    //void RightUseSkill()
    //{
    //    if (playerSkillCheck.Q_skillCheck && RWindC)
    //    {
    //        RightWindUsing.SetActive(true);
    //        LWindC = false;
    //    }
    //    else if (playerSkillCheck.E_skillCheck || playerSkillCheck.R_skillCheck)
    //    {
    //        LeftWindUsing.SetActive(false);
    //        LWindC = true;
    //    }

    //    if (playerSkillCheck.E_skillCheck && RCloudC)
    //    {
    //        RightCloudUsing.SetActive(true);
    //        LCloudC = false;
    //    }
    //    else if (playerSkillCheck.Q_skillCheck || playerSkillCheck.R_skillCheck)
    //    {
    //        LeftCloudUsing.SetActive(false);
    //        LCloudC = true;
    //    }

    //    if (playerSkillCheck.R_skillCheck && RRainC)
    //    {
    //        LeftRainUsing.SetActive(true);
    //        LRainC = false;
    //    }
    //    else if (playerSkillCheck.Q_skillCheck || playerSkillCheck.E_skillCheck)
    //    {
    //        LeftRainUsing.SetActive(false);
    //        LRainC = true;
    //    }
    //}

    void ItemUse()
    {
        if(Input.GetKey(HP_itemKey))
        {
            StartCoroutine(HPitemCor());
        }
        
        if(Input.GetKey(Respwan_ItemKey))
        {
            player.gameObject.SetActive(true);
            RESPWAN_item.SetActive(false);
        }

        if(Input.GetKey(DarkPillItem__itemkey))
        {
            StartCoroutine(DarkPillCor());
        }
    }

    IEnumerator HPitemCor()
    {
        while(curHp.currentHealth < curHp.maxHealth)
        {
           curHp.currentHealth += 0.1f;
           yield return new WaitForEndOfFrame();
        }
        HP_item.SetActive(false);
    }

    IEnumerator DarkPillCor()
    {
        while(Gauge.sGauge < DarkPill.maxGauge)
        {
            Gauge.sGauge += 0.1f;
            yield return new WaitForEndOfFrame();
        }
        DARKPILL_item.SetActive(false);
    }

    
}
