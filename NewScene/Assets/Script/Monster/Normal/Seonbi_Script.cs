using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Seonbi_Script : Enemy
{
    //private NavMeshAgent agent = null;
    private Animator animator;

    [SerializeField]
    float SetY;
    RaycastHit hit;

    bool isattack;
    [SerializeField]
    bool DontMove;

    public Rigidbody SeonbiBullet; 

    public float monsterhp;
    protected override void Awake()
    {
        base.Awake();
        isattack = false;
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        monsterMove();
        NotDamaged();
        DieMonster();
    }

    protected override void monsterMove()
    {
        animator.SetBool("Move", true);
        base.monsterMove();


        Vector3 targety = targetTransform.position;
        targety.y = SetY;

        //현재 경로에서 목표 지점까지 남아있는 거리
        if (agent.remainingDistance <= 5)
        {
            if (!isattack)
            {
                transform.LookAt(targety);
                isattack = true;
                StartCoroutine("attacker");

            }

        }

        //float time = Time.deltaTime;

        //Vector3 rayspawn = transform.position;
        //rayspawn.y = SetY + 0.5f; //레이 바닥에서 부터 쏘게만듬

        //Vector3 targety = targetTransform.position;
        //targety.y = SetY;

        //transform.LookAt(targety);
        ////현재는 플레이어를 쫒다가 레이케스트에 닿으면 할퀴기 공격 시작
        //Debug.DrawRay(rayspawn, transform.forward * MaxDistance, Color.blue, 0.05f);

        //if (Physics.Raycast(rayspawn, transform.forward, out hit, MaxDistance))
        //{
        //    if (hit.collider.tag == "Main_gangrim") //플레이어에게 레이 닿으면 정지하고 총알 공격
        //    {
        //        Debug.Log("Main tag");
        //        DontMove = true;
        //        if (!isattack)
        //        {
        //            isattack = true;
        //            transform.LookAt(targety);
        //            StartCoroutine("attacker");

        //        }
        //    }

        //}
        //else
        //{
        //    DontMove = false;
        //}

        //if (!DontMove)
        //{
        //    animator.SetBool("Move", true);
        //    //agent.destination = targety;
        //    transform.LookAt(targety);
        //    transform.position = Vector3.MoveTowards(gameObject.transform.position, targety, chasespeed * Time.deltaTime);//3번째 값 돌진 속도
        //}
        //else
        //{
        //    animator.SetBool("Move", false);
        //}
    }


    IEnumerator attacker()
    {
        Vector3 target_ = transform.position;
        target_.y = SetY+0.5f;

        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.9f);
        Rigidbody ThrowRockrigid = Instantiate(SeonbiBullet, target_, transform.rotation);
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(0.7f);
        isattack = false;
    }

}
