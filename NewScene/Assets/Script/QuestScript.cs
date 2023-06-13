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
    [SerializeField]
    private TextMeshProUGUI QuestText;
    [TextArea]
    public string[] tTextList;
    public List<GameObject> monsterParentsList = new List<GameObject>();
    public GameObject[] monsterParents;

    //public ChatData[] chatList;
    private int MonsterCount =0;

    private void Start()
    {
        QuestText.text = tTextList[0];
    }
    // Update is called once per frame
    void Update()
    {
       QuestText.text = tTextList[MonsterCount];
    }

    public void CountUp()
    {
        MonsterCount += 1;
    }
}
