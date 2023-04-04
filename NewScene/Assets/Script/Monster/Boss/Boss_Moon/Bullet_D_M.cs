using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_D : MonoBehaviour
{
    public GameObject Rock;
    [SerializeField]
    float spawntime;
    float time;

    [SerializeField]
    bool Spawner;
    [SerializeField]
    bool Attacker;
    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        if (Spawner)
        {
            time += Time.deltaTime;

            if (time > spawntime)
            {
                Debug.Log("sssssssss");
                //GameObject Skill1 = Instantiate(Rock, transform.position, transform.rotation);
                Rock.SetActive(true);
                time = 0;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Main_gangrim")
        {

            if (Attacker)
            {

                Debug.Log("Damage for ballet");
                Destroy(gameObject);
                //대충 플레이어가 대미지 입었다는 코드 sendMessage등 으로 Player 스크립트에 접근
                //나중에 플레이어 체력 정해지면 추가
            }


        }

    }
}
