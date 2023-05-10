using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Floor_bullet : MonoBehaviour
{
    //몬스터 문의 장판 공격 총알
    //플레이어와 접속 시 도트딜
    //지속시간 15초
    [SerializeField]
    GameObject FloorObj;
    float time;
    bool startattaack;
    bool isattack;

    public float floorBulletDamage;
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= 5f) //10초 뒤에 파괴
        {
            Destroy(FloorObj);
        }

        if (startattaack)
        {
            if (!isattack)
            {
                isattack = true;

                StartCoroutine("PlayerAttackTime");
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
        yield return new WaitForSeconds(2f); //도트딜 시간
        isattack = false;
    }
}
