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

    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= 7f) //10초 뒤에 파괴
        {
            Destroy(FloorObj);
        }

        if (startattaack)
        {
            if (!isattack)
            {
                isattack = true;
                //플레이어 스크립트에 도트딜 매서드 추가후 불러오기
                ////현재 코루틴 시간이 지날때 마다 매서드 작동
                //대충 플레이어 데미지 매서드만 있으면 코루틴 시간마다 도트딜 들어옴

                //********************************** 여기에 플레이어 데미지 매서드나 플레이어 체력 관련 들어가면 됨
                Debug.Log("door floor damage");


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
        yield return new WaitForSeconds(2f); //도트딜 시간
        isattack = false;
    }
}
