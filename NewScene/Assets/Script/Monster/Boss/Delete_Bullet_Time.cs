using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Bullet_Time : MonoBehaviour
{
    public float DestoryTime;
    float t; //Ÿ�̸�

    void Update()
    {
        t += Time.deltaTime;

        if (t > DestoryTime)
        {
            Destroy(gameObject);
            t = 0;
        }

    }
}
