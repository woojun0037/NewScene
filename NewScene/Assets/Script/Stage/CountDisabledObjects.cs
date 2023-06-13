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


        // �θ� ������Ʈ�� ��� �ڽ� ������Ʈ�� ��ȸ�ϸ鼭
        foreach (Transform child in transform)
        {
            // �ڽ� ������Ʈ�� ��Ȱ��ȭ�� ��� ī��Ʈ�� ������Ų��
            if (!child.gameObject.activeSelf)
            {
                count++;
            }
        }

        // ��Ȱ��ȭ�� ������Ʈ�� ���� ����Ѵ�
        

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
