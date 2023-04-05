using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Script : MonoBehaviour
{
    [SerializeField]
    GameObject attacker_Col;
    private Animator animator;
    [SerializeField]
    float SetY;
    bool isattack;
    bool DontMove;
    Transform targettransform;

    [SerializeField]
    float MaxDistance;
    [SerializeField]
    float chasespeed;
    RaycastHit hit;

    bool meetplayer;

    public float monsterhp;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
        targettransform = GameObject.FindWithTag("Main_gangrim").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayspawn = transform.position;
        rayspawn.y = SetY; //���� �ٴڿ��� ���� ��Ը���

        Vector3 targety = targettransform.position;
        targety.y = SetY;


        Debug.DrawRay(rayspawn, transform.forward * MaxDistance, Color.blue, 0.05f);

        if (Physics.Raycast(rayspawn, transform.forward, out hit, MaxDistance))
        {
            if (hit.collider.tag == "Main_gangrim") //�÷��̾�� ���� ������ �����ϰ� �Ѿ� ����
            {
                Debug.Log("Main tag");
                DontMove = true;
                if (!isattack)
                {
                    isattack = true;
                    transform.LookAt(targety);
                    animator.SetBool("Attack", true);
                    StartCoroutine("attacker");

                }
            }

        }
        else
        {
            DontMove = false;
        }

        if (!DontMove)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Move", true);
            //agent.destination = targety;
            transform.LookAt(targety);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targety, chasespeed * Time.deltaTime);//3��° �� ���� �ӵ�
        }
        else
        {
            animator.SetBool("Move", false);
        }

    }

    void MonsterDamage() //�÷��̾� ��ũ��Ʈ���� �ҷ���
    {
        Debug.Log("MonsterDamage()");

        if (monsterhp > 1)
        {
            monsterhp--;
        }
        else
        {
            MonsterDie();
        }
        Debug.Log(monsterhp);
    }

    void MonsterDie()
    {
        Destroy(gameObject);
    }

    IEnumerator attacker()
    {
        animator.SetBool("Attack", true);
        attacker_Col.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        attacker_Col.SetActive(false);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(0.7f);
        isattack = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Main_gangrim")
        {
            //�÷��̾ �ǰ� �ż��� �ҷ���
            //playercollider = true;

            Debug.Log("player collision");
        }
    }
}
