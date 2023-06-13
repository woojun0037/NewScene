using System;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class QuestScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI QuestText;
    [TextArea] public string[] tTextList;

    [SerializeField] private int MonsterCount = 0;
    [SerializeField] int MaxTextcount;
    float time = 0;
    public GameObject NextStage;

    private void Start()
    {
        MaxTextcount = tTextList.Length;
        QuestText.text = tTextList[0];
    }
    // Update is called once per frame
    void Update()
    {
        if(MonsterCount + 2 >= MaxTextcount)
        {
            time += Time.deltaTime;
            if(time > 3)
            {
                Debug.Log("time > 3");
                QuestText.text = tTextList[MaxTextcount -1];
                NextStage.SetActive(true);
            }
            else
                QuestText.text = tTextList[MonsterCount];

        }
        else
        {
            QuestText.text = tTextList[MonsterCount];
        }
    }

    public void CountUp() //CountDisabledObjects script
    {
        MonsterCount += 1;
    }
}
