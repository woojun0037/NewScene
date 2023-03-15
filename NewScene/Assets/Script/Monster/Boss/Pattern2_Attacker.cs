using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2_Attacker : MonoBehaviour
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
                //���� �÷��̾ ����� �Ծ��ٴ� �ڵ� sendMessage�� ���� Player ��ũ��Ʈ�� ����
                //���߿� �÷��̾� ü�� �������� �߰�
            }


        }

    }
}
