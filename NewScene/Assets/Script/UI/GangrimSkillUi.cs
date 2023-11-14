using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GangrimSkillUi : MonoBehaviour
{
    public static GangrimSkillUi instance;

    public HealthManager curHp;
    public Gauge DarkPill;
    public GameObject player;
    public PlayerSkill playerSkillCheck;
    public PropertySkill PropertySkillCheck;
    public Main_Player main;

    public DOTweenAnimation windDot;
    public DOTweenAnimation tornadoDot;
    public DOTweenAnimation rainDot;
    public DOTweenAnimation dashDot;

    public DOTweenAnimation leftDot;
    public DOTweenAnimation rightDot;


    public Image dashHide;

    public GameObject option;
    public GameObject MouseOptionDetail;

    public GameObject skillKey;

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
    public bool HPitemOn = false;

    public GameObject RESPWAN_item;
    public KeyCode Respwan_ItemKey;
    public bool RESPWAN_itemOn = false;

    public GameObject DARKPILL_item;
    public KeyCode DarkPillItem__itemkey;
    public bool DarkPillItemOn = false;

    public bool isOption;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DarkPill.skill = playerSkillCheck;

            spriteDictionary = new Dictionary<string, Sprite>();
            spriteDictionary.Add("wind", uiSprites[0]);
            spriteDictionary.Add("tornado", uiSprites[1]);
            spriteDictionary.Add("rain", uiSprites[2]);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
        //abilityImage1.fillAmount = 0;
        //abilityImage2.fillAmount = 0;
        //abilityImage3.fillAmount = 0;
        dashHide.fillAmount = 0;
    }

    void Update()
    {
        Option();
        CoolTimeUpdate();
        ItemUse();
    }

    public void CoolTimeUpdate()
    {
        abilityImage1.fillAmount = playerSkillCheck.WindSkillTime / playerSkillCheck.WindSkillCool;
        abilityImage2.fillAmount = playerSkillCheck.TornadoSkillTime / playerSkillCheck.TornadoSkillCool;
        abilityImage3.fillAmount = playerSkillCheck.RainSkillTime / playerSkillCheck.RainSkillCool;
        dashHide.fillAmount = playerSkillCheck.DashSkillTime / playerSkillCheck.DashSkillCool;
    }


    private void Option()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOption = !isOption;

            if (isOption)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
            option.SetActive(isOption);
        }
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void GameTitle()
    {
        isOption = !isOption;
        option.SetActive(isOption);

        SceneManager.LoadScene("Title");
    }

    public void mouseGamDoe()
    {
        MouseOptionDetail.SetActive(true);
    }

    public void mouseGamoeOff()
    {
        MouseOptionDetail.SetActive(false);
    }

    public void SkillUiInit()
    {
        leftDot.DORestart();
        leftDot.tween.OnComplete(() =>
        {
            uiKeys[0] = "0";
            uiImages[0].sprite = null;
            uiImages[0].enabled = false;
        });
        rightDot.DORestart();
        rightDot.tween.OnComplete(() =>
        {
            uiKeys[1] = "0";
            uiImages[1].sprite = null;
            uiImages[1].enabled = false;
            skillKey.SetActive(false);
            uiImages[2].enabled = false;
        });
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
        else if (uiKeys[1] == "0")
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

        if ((uiKeys[0] == "wind" && uiKeys[1] == "tornado") ||
            (uiKeys[0] == "tornado" && uiKeys[1] == "wind"))
        {
            skillKey.SetActive(true);
            uiImages[2].sprite = uiSprites[3];
            uiImages[2].enabled = true;
        }
        else if ((uiKeys[0] == "wind" && uiKeys[1] == "rain") ||
               (uiKeys[0] == "rain" && uiKeys[1] == "wind"))
        {
            skillKey.SetActive(true);
            uiImages[2].sprite = uiSprites[4];
            uiImages[2].enabled = true;
        }
    }

    void ItemUse()
    {
        if (Input.GetKey(HP_itemKey) && HPitemOn)
        {
            HP_item.SetActive(false);
            StartCoroutine(HPitemCor());
        }

        if (Input.GetKey(Respwan_ItemKey) && RESPWAN_itemOn)
        {
            curHp.HPbar.fillAmount = 1;
            main.currentHp = main.HP;

            player.gameObject.SetActive(true);
            RESPWAN_item.SetActive(false);
        }

        if (Input.GetKey(DarkPillItem__itemkey) && DarkPillItemOn)
        {
            DARKPILL_item.SetActive(false);
            StartCoroutine(DarkPillCor());
        }
    }

    public IEnumerator HPitemCor()
    {
        Debug.Log("체력 회복" + curHp.player.currentHp);
        if (main == null) main = FindObjectOfType<Main_Player>();

        while (curHp.player.currentHp < curHp.player.HP)
        {
            curHp.player.currentHp += Time.deltaTime;
            curHp.HPbar.fillAmount = curHp.player.currentHp / curHp.player.HP;

            yield return null;
        }
        main.HP = curHp.player.currentHp;
        Debug.Log("체력 회복" + curHp.player.currentHp);
        HPitemOn = false;
    }

    IEnumerator DarkPillCor()
    {
        Debug.Log("다크 회복" + Gauge.sGauge);

        while (Gauge.sGauge < DarkPill.maxGauge)
        {
            Gauge.sGauge += 0.1f;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("다크 회복" + Gauge.sGauge);
        DarkPillItemOn = false;
        DARKPILL_item.SetActive(false);
    }
}
