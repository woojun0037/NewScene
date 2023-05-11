using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIcontroller : MonoBehaviour
{
    public GameObject[] hideSkillbuttons;
    public GameObject[] textPros;
    public TextMeshProUGUI[] hideSkillTimeTexts;
    public Image[] hideSkillimage;
    private bool[] isHideSkills = { false, false, false, false };
    private float[] skillTimes = { 3, 6, 9, 12 };
    private float[] getSkillTimes = { 0, 0, 0, 0 };

    void Start()
    {
        for(int i=0;i < textPros.Length; i++)
        {
            hideSkillTimeTexts[i] = textPros[i].GetComponent<TextMeshProUGUI>();
            hideSkillbuttons[i].SetActive(false);
        }
    }

    void Update()
    {
        HideSkillCheck();
    }


    public void HideSkillSetting(int skillNum)
    {
        hideSkillbuttons[skillNum].SetActive(true);
        getSkillTimes[skillNum] = skillTimes[skillNum];
        isHideSkills[skillNum] = true; 
    }

    private void HideSkillCheck()
    {
        if (isHideSkills[0])
        {
            StartCoroutine(SkillCheckTimeCor(0));
        }

        if (isHideSkills[1])
        {
            StartCoroutine(SkillCheckTimeCor(1));
        }

        if (isHideSkills[2])
        {
            StartCoroutine(SkillCheckTimeCor(2));
        }

        if (isHideSkills[3])
        {
            StartCoroutine(SkillCheckTimeCor(3));
        }
    }


    IEnumerator SkillCheckTimeCor(int skillNum)
    {
        yield return null;

        if (getSkillTimes[skillNum] > 0)
        {
            getSkillTimes[skillNum] -= Time.deltaTime;

            if (getSkillTimes[skillNum] < 0)
            {
                getSkillTimes[skillNum] = 0;
                isHideSkills[skillNum] = false;
                hideSkillbuttons[skillNum].SetActive(false);
            }

            hideSkillTimeTexts[skillNum].text = getSkillTimes[skillNum].ToString("00");

            float time = getSkillTimes[skillNum] / skillTimes[skillNum];
            hideSkillimage[skillNum].fillAmount = time;
        }
    }
}
