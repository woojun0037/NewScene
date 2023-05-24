using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaveCheck : MonoBehaviour
{
    public List<GameObject> monsterParentsList = new List<GameObject>();
    public GameObject[] monsterParents;

    private void Start()
    {
        foreach (GameObject monsterParent in monsterParents)
        {
            monsterParent.transform.position = transform.position;
            if (!monsterParent.activeSelf)
            {
                monsterParent.SetActive(true);
            }
        }

        // monsterParents 배열에 있는 모든 오브젝트들의 자식들을 리스트에 추가하기
        foreach (GameObject monsterParent in monsterParents)
        {

            foreach (Transform child in monsterParent.transform)
            {
                monsterParentsList.Add(child.gameObject);
            }

            foreach (Transform child in monsterParent.transform)
            {
                GameObject monsterRabbit = child.gameObject;
                monsterRabbit.SetActive(true);
            }
        }
    }

    private void Update()
    {
        List<GameObject> deadMonsters = new List<GameObject>();

        foreach (GameObject monster in monsterParentsList)
        {
            if (!monster.activeSelf)
                deadMonsters.Add(monster);
        }


        foreach (GameObject deadMonster in deadMonsters)
        {
            monsterParentsList.Remove(deadMonster);
        }


        if (monsterParentsList.Count == 0)
        {
            gameObject.SetActive(false);
        }
    }
}