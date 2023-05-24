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

    public Dictionary<string, Sprite> spriteDictionary;
    [Header("SkillUseCheck")]
    public Image[] uiImages;
    public Sprite[] uiSprites;
    public string[] uiKeys;


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

    private void Awake()
    {
        spriteDictionary = new Dictionary<string, Sprite>();
        spriteDictionary.Add("wind", uiSprites[0]);
        spriteDictionary.Add("cloud", uiSprites[1]);
        spriteDictionary.Add("rain", uiSprites[2]);
    }

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

    public void CurrentSkillUI(string key)
    {
        if (uiKeys[0] == key) return;
        if (uiKeys[1] == key) return;

        if (uiKeys[0] == "0")
        {
            uiKeys[0] = key;
            uiImages[0].sprite = spriteDictionary[key];
            uiImages[0].enabled = true;
        }
        else if(uiKeys[1] == "0")
        {
            uiKeys[1] = key;
            uiImages[1].sprite = spriteDictionary[key];
            uiImages[1].enabled = true;
        }
        else if (uiKeys[0] != "0" && uiKeys[1] != "0")
        {
            uiKeys[0] = key;
            uiKeys[1] = "0";
            uiImages[0].sprite = spriteDictionary[key];
            uiImages[1].sprite = null;
            uiImages[1].enabled = false;
        }
    }

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
