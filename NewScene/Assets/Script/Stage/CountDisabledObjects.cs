using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDisabledObjects : MonoBehaviour
{
    public int MaxCount;
    int count = 0;
    public bool countMonster;

    private void Start()
    {
        MaxCount = transform.childCount;
    }
    private void Update()
    {


        // 부모 오브젝트의 모든 자식 오브젝트를 순회하면서
        foreach (Transform child in transform)
        {
            // 자식 오브젝트가 비활성화된 경우 카운트를 증가시킨다
            if (!child.gameObject.activeSelf)
            {
                count++;
            }
        }

        // 비활성화된 오브젝트의 수를 출력한다
        

        if(count == MaxCount)
        {
            if (countMonster)
            {
                FindObjectOfType<QuestScript>().CountUp();
                gameObject.SetActive(false);
            }
            else
                gameObject.SetActive(false);
        }
    }

}
