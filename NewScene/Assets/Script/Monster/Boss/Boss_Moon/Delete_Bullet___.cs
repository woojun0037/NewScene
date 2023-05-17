using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Bullet___ : MonoBehaviour
{
    public float DestoryTime;
    float t; //타이머

    void Update()
    {
        t += Time.deltaTime;

        if (t > DestoryTime)
        {
            gameObject.SetActive(false);
            t = 0;
        }

    }
}
