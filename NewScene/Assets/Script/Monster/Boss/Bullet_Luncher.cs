using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Luncher : MonoBehaviour
{
    public GameObject TapObj; // 부모 오브젝트 - 전체 삭제용
    public Transform PrefabToSpwan; // 부모 오브젝트 - 전체 삭제용 (스폰 부모)
    public Rigidbody PrefabAttacker;
    public float AttactSpawnTime;

    bool time;
    public float speed = 1;
    Rigidbody thisrigidy;
    public float removeTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        AttactSpawnTime = AttactSpawnTime * Time.deltaTime * 1;
        thisrigidy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Destroy(TapObj, removeTime);

        thisrigidy.velocity = transform.forward * Time.deltaTime * speed;
        transform.position = transform.position + new Vector3(1, 0, 0) * Time.deltaTime * speed;
        //rigid로 받고 몬스터 오브젝트 자체에서 발사해서 위 코드 필요 없을 듯.. 
        //위코드 사용하려면 freeze 오브젝트 Z,Y,X 헤야되는데 그러면 플레이어 포지션 받아서 몬스터 오브젝트 코드같이(중복)

        abc();
    }
    void abc()
    {
        //Vector3 attactspawnposition = transform.position;
        // attactspawnposition.z = 0.1f * Time.deltaTime;

        if (!time)
        {
            //밑 코드 transform.position 에서 PrefabToSpwan.transform.position로 변경//총알이 지정한 위치에서 소환
            Rigidbody attacker = Instantiate(PrefabAttacker, PrefabToSpwan.transform.position, Quaternion.identity);

            // Instantiate(prefabToSpwan, startPos, Quaternion.identity);
            attacker.transform.SetParent(PrefabToSpwan);

            attacker.velocity = transform.forward * Time.deltaTime * speed;
            StartCoroutine("spawntimes");
            time = true;
        }
    }

    IEnumerator spawntimes()
    {
        yield return new WaitForSeconds(AttactSpawnTime);
        time = false;
    }

}