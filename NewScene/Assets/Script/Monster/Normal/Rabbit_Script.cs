using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Script : Enemy
{
    [SerializeField]
    GameObject attacker_Col;
    private Animator animator;
    [SerializeField]
    float SetY;
    bool isattack;
    bool DontMove;
    Transform targettransform;

    RaycastHit hit;
    [SerializeField]
    private float Distance;
    bool meetplayer;
    float time;
    
    public float monsterhp;
    protected override void Awake()
    {
        base.Awake();
        isattack = false;
    }

    protected override void Start()
    {
        base.Start();
        time = 0;
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
        targettransform = GameObject.FindWithTag("Main_gangrim").transform;
    }

    // Update is called once per frame
    void Update()
    {
        DieMonster();
        monsterMove();
        NotDamaged();
    }


    protected override void monsterMove()
    {
        animator.SetBool("Move", true);
        base.monsterMove();

        Vector3 targety = targetTransform.position;
        targety.y = SetY;

        //���� ��ο��� ��ǥ �������� �����ִ� �Ÿ�
        if (agent.remainingDistance <= 2)
        {
            if (!isattack)
            {
                transform.LookAt(targety);
                isattack = true;
                StartCoroutine("attacker");
            }
        }


        //Distance = Vector3.Distance(targettransform.transform.position, transform.position);

        //time += Time.deltaTime;
        //Vector3 rayspawn = transform.position;
        //rayspawn.y = SetY; //���� �ٴڿ��� ���� ��Ը���

        //Vector3 targety = targettransform.position;
        //targety.y = SetY;


        //Debug.DrawRay(rayspawn, transform.forward * MaxDistance, Color.blue, 0.05f);

        //if (Physics.Raycast(rayspawn, transform.forward, out hit, MaxDistance))
        //{
        //    if (hit.collider.tag == "Main_gangrim") //�÷��̾�� ���� ������ �����ϰ� �Ѿ� ����
        //    {
        //        if (Distance > 1f)
        //        {
        //            time = 0;
        //            //Debug.Log("Main tag");
        //            DontMove = true;
        //            if (!isattack)
        //            {
        //                isattack = true;
        //                transform.LookAt(targety);
        //                animator.SetBool("Attack", true);
        //                StartCoroutine("attacker");

        //            }
        //        }
        //        else  //�Ÿ��� ���� �̻� ������ ����
        //        {

        //            animator.SetBool("Idle", true);
        //            animator.SetBool("Move", false);

        //            transform.LookAt(targety);


        //        }

        //    }

        //}
        //else
        //{
        //    DontMove = false;
        //}

        //if (!DontMove)
        //{
        //    if (Distance > 1f && !meetplayer)
        //    {
        //        animator.SetBool("Idle", true);
        //        animator.SetBool("Move", true);
        //        //agent.destination = targety;

        //        transform.position = Vector3.MoveTowards(gameObject.transform.position, targety, chasespeed * Time.deltaTime);//3��° �� ���� �ӵ�
        //        transform.LookAt(targety);
        //    }

        //}
        //else
        //{
        //    animator.SetBool("Move", false);
        //}

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

}
