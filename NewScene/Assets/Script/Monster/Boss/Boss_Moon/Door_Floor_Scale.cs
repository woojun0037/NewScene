using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Floor_Scale : MonoBehaviour
{
    //받아온 게임 오브젝트 만큼 커짐
    [SerializeField]
    GameObject FloorBullet; 

    [SerializeField]
    Transform FloorScale;

    [SerializeField]  float scalespeed =2f; //커지는 속도

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x <= FloorScale.localScale.x )
        {
            transform.localScale = new Vector3
                (transform.localScale.x + 1f * scalespeed * Time.deltaTime, FloorScale.localScale.y, transform.localScale.z + 1f * scalespeed * Time.deltaTime);
        }
        else
        {
            FloorBullet.SetActive(true);
            Destroy(gameObject);
        }

    }
}
