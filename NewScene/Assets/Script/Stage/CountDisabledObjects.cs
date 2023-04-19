using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDisabledObjects : MonoBehaviour
{
    public int MaxCount;

    private void Start()
    {
        MaxCount = transform.childCount;
    }
    private void Update()
    {
        int count = 0;

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
        Debug.Log("Number of disabled objects: " + count);

        if(count == MaxCount)
        {
            gameObject.SetActive(false);   
            Debug.Log("count == MaxCount");
        }
    }

}
