using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Spawn : MonoBehaviour
{
    public GameObject prefabToSpwan;
    public float repeatInterval; //이 초마다 prefabToSpwan 생성

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
        return null; // prefabToSpwan 이 null이면 에딛터에서 제대로 설정하지 않았자는 null 반환
    }

}