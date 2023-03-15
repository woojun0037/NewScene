using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Spawn : MonoBehaviour
{
    public GameObject prefabToSpwan;
    public float repeatInterval; //�� �ʸ��� prefabToSpwan ����

    void Start()
    {
        if (repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }
    }
    public GameObject SpawnObject()
    {
        if (prefabToSpwan != null)
        {
            return Instantiate(prefabToSpwan, transform.position, Quaternion.identity);
        }
        return null; // prefabToSpwan �� null�̸� �����Ϳ��� ����� �������� �ʾ��ڴ� null ��ȯ
    }

}