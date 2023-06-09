﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Floor_bullet : MonoBehaviour
{
    float time;
    bool startattaack;
    bool isattack;

    public float floorBulletDamage;
    private void OnEnable()
    {
        isattack = false;
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= 3f)
        {
            if (startattaack)
            {
                if (!isattack)
                {
                    isattack = true;
                    StartCoroutine("PlayerAttackTime");
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other) //플레이어가 들어오면 bool 호출 후 도트딜
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            startattaack = true;     
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Main_gangrim")
        {
            startattaack = false;
        }

    }

    IEnumerator PlayerAttackTime()
    {
        FindObjectOfType<HealthManager>().HurtPlayer(floorBulletDamage);
        yield return new WaitForSeconds(1f); //도트딜 시간
        isattack = false;

        yield return new WaitForSeconds(4f);
        time = 0;
        gameObject.SetActive(false);
    }
}
