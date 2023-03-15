using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Luncher : MonoBehaviour
{
    public GameObject TapObj; // �θ� ������Ʈ - ��ü ������
    public Transform PrefabToSpwan; // �θ� ������Ʈ - ��ü ������ (���� �θ�)
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
        //rigid�� �ް� ���� ������Ʈ ��ü���� �߻��ؼ� �� �ڵ� �ʿ� ���� ��.. 
        //���ڵ� ����Ϸ��� freeze ������Ʈ Z,Y,X ��ߵǴµ� �׷��� �÷��̾� ������ �޾Ƽ� ���� ������Ʈ �ڵ尰��(�ߺ�)

        abc();
    }
    void abc()
    {
        //Vector3 attactspawnposition = transform.position;
        // attactspawnposition.z = 0.1f * Time.deltaTime;

        if (!time)
        {
            //�� �ڵ� transform.position ���� PrefabToSpwan.transform.position�� ����//�Ѿ��� ������ ��ġ���� ��ȯ
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